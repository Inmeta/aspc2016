install.packages("jsonlite")
library('jsonlite')

setwd("C:/Git/ASPC2016/R")
stat <- read.table("C:/Git/ASPC2016/R/FylkeKomunneCrimeNumber.tsv", encoding="UTF-8", sep = "\t", header = T)
stat = stat[,-1]


komunneName <- as.vector(stat$Komunne)
crimerate <- stat['X2014']

coord <- c(0,0,0)
a <- 0
i = 12
for (i in (1:length(komunneName)))
{
  addr = paste("http://maps.google.com/maps/api/geocode/json?address=", komunneName[i], ",Norway", sep = '')

  error <- NULL
  tryCatch(
    {
      res <- fromJSON(addr)
      a <- res$results$geometry$location
    }, error = function(err)
    {
      error <- err
    } )
  if (is.null(error) && !is.null(a))
  {
    newrow <- cbind(as.vector(a), crimerate[i,])
    coord <- rbind(coord, newrow)
  }
}
crimeByCoord <- as.matrix(coord[-1,])
colnames(crimeByCoord) <- c("lat", "long","N")
write.csv(crimeByCoord, file = "./crimeByCoord.csv")
