setwd("C:/Git/ASPC2016")
stat <- read.table("C:/Git/ASPC2016/FylkeKomunneCrimeNumber.tsv", encoding="UTF-8", sep = "\t", header = T)
a = stat[,-1]

Year = c(2008,2009,2010,2011,2012,2013,2014)
komunneName <- as.vector(a$Komunne)
crime <- data.frame(a[,-1])

#year = 2016
newyear = c(2015)

#plot by komunneId
ind <- which(komunneName == "Hvaler")
ind = c(1)
Number <- as.vector(t(crime[ind,]))

plot(Year, Number, type = "b", ylim = c(min(Number)*0.5, max(Number) * 1.5), xlim = c(2008, 2016))

model1 = lm(Number ~ Year)
model1
prediction <- predict(model1)
points(rep(Year, length(ind)), prediction, col = "dark red", type="b")

newdata <- data.frame(Year=newyear)
prediction <- predict(model1, newdata = newdata)
points(x = c(newyear), y = prediction, col="red")

# model2 <- lm(Number ~ poly(Year,2))
# prediction <- predict(model2)
# points(Year, prediction, col = "dark green", type="b")
# 
# newdata <- data.frame(Year=newyear)
# prediction <- predict(model2, newdata = newdata)
# points(x = c(newyear), y = prediction, col="green")
# prediction
