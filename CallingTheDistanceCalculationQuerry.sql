




DECLARE @temp table ( LattitudeOfCustomer	REAL,LongitudeOfCustomer	REAL)
		INSERT INTO @temp
						
							SELECT 
										CAST(lat AS REAL) AS lat, CAST(long AS REAL) AS long 
							FROM 
										geolocation AS GL JOIN tblCustomerMASter AS CM
										ON GL.ID=CM.LocationID and CM.ID=2
						






--DECLARE @temp2 table(Kms REAL,DealerID INT)
--		INSERT INTO @temp2
SELECT dbo.DistanceBetween
 (
	(SELECT LattitudeOfCustomer  FROM @temp ) ,
	(SELECT  LongitudeOfCustomer  FROM @temp ),
	lat,
	long) AS Kms, DM.ID,DM.Name
	FROM geolocation  as GL JOIN tblDealerMaster as DM 
	ON GL.ID=DM.Location
ORDER BY Kms

select * from @temp2
select * from tblDealerMaster
select * from tblDealerPoints


select * from ACU_UserMaster
select * from tblrequestheader
select * from tblRequestAccessories
select * from tblRequestDealer