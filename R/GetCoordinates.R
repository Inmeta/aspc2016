install.packages("jsonlite")
library('jsonlite')

setwd("C:/Git/ASPC2016/R")
stat <- read.table("C:/Git/ASPC2016/R/FylkeKomunneCrimeNumber.tsv", encoding="UTF-8", sep = "\t", header = T)
stat = stat[,-1]

komunneName <- as.vector(stat$Komunne)
crimerate <- stat
colnames(crimerate) <- c('Name',2014,2013,2012,2011,2010,2009,2008)

coord <- c(0,0,0,0)
a <- 0
i = 1
j = 1
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
  
  if (length(a) == 2)
  {
    #cat(unlist(a))
    newrow <- c(0,0,0,0)
    
    for (j in (2008:2014))
    {
      nr <- cbind(cbind(as.vector(a), crimerate[i,toString(j)]), j-2000)
      colnames(nr) <- c("lat", "long","N","Y")
      newrow <- rbind(newrow, nr)
    }
    newrow <- newrow[-1,]
    
    coord <- rbind(coord, newrow)
  }
  closeAllConnections()
}
crimeByCoord <- as.matrix(coord[-1,])
colnames(crimeByCoord) <- c("lat", "long","N","Y")
write.csv(crimeByCoord, file = "./crimeByCoord.csv", row.names = F, quote = F)
