			DECLARE @Temp TABLE(ID int,ACCIDorCHARGETYPEID int,IsChargeType bit,Value1 float,Value2 float,Value3 float)
				INSERT INTO @Temp
				SElect Value1.ID, Value1.ACCIDorCHARGETYPEID,Value1.IsChargeType,Value1.QuoteValue ,Value2.QuoteValue ,Value3.QuoteValue 
				FROM
				(

					select  ID,QuoteValue,OptionID,ACCIDorCHARGETYPEID,IsChargeType
					From tblQuotationDetails
					where optionId =1
					


				)Value1
				inner join
				(
					select  ID,QuoteValue,OptionID,ACCIDorCHARGETYPEID,IsChargeType
				From tblQuotationDetails
				where optionId =2


				) Value2
				
				on Value1.ACCIDorCHARGETYPEID=Value2.ACCIDorCHARGETYPEID and Value1.IsChargeType=Value2.IsChargeType
				inner join
							(
								select  ID,QuoteValue,OptionID,ACCIDorCHARGETYPEID,IsChargeType
							From tblQuotationDetails
							where optionId =3


							) Value3
				on Value3.ACCIDorCHARGETYPEID=Value2.ACCIDorCHARGETYPEID and Value3.IsChargeType=Value2.IsChargeType 
				order by Value1.ID


select * from @Temp

		

	SELECT 
			CTM.ID as ID,'Recommended Retail Price Exc GST' as [Key],'' as Specification,'false' as IsAccessory,'true' as IsChargeType,a.Value1,a.Value2,a.Value3 
	FROM
			dbo.tblChargesTypesMaster as CTM JOIN @Temp  as a on a.ACCIDorCHARGETYPEID=CTM.ID
	WHERE	
				Type='Recommended Retail Price Exc GST'
	UNION ALL

	SELECT 
			'' as ID,'Additional Accessories' as [Key],'' as Specification,'false' as IsAccessory,'false' as IsChargeType,''as value1,'' as Value2,'' 


UNION ALL
	SELECT 
			AM.ID as ID, AM.[Name] AS [Key]
			,RA.AccessorySpecification AS Specification,'true' as IsAccessory,'false' as IsChargeType,a.Value1,a.Value2,a.Value3 --, '' as  Value1,'' as  Value2
			
	FROM	
			tblRequestHeader RH
			JOIN tblRequestAccessories RA ON RH.ID = RA.RequestID
			JOIN tblAccessoriesMaster AM ON AM.ID = RA.AccessoryID
			JOIN @Temp as a on a.ACCIDorCHARGETYPEID=RA.AccessoryID and a.IsChargeType=0
	WHERE
			RH.ID = 19
			AND AM.IsParameter = 0



	UNION ALL
	
		SELECT 
			
			'' as ID,'Fixed Charges' as [Key],'' as Specification,'false' as IsAccessory,'false' as IsChargeType,'','',''

		UNION ALL

		SELECT 
					CT.ID as ID,CT.[Key],'' as Specification,'false' as IsAccessory,'true' as IsChargeType,a.Value1,a.Value2,a.Value3 
		FROM
						dbo.VWForQuotationData as CT JOIN @Temp as a on a.ACCIDorCHARGETYPEID=CT.ID and a.IsChargeType=1