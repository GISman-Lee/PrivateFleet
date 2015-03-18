SElect a.ID, a.ACCIDorCHARGETYPEID,b.QuoteValue as 'Value1',a.QuoteValue as 'Value2',a.IsChargeType
FROM
(

select  ID,QuoteValue,OptionID,ACCIDorCHARGETYPEID,IsChargeType
	From tblQuotationDetails
	where optionId=1
	


) b
inner join
(
	select  ID,QuoteValue,OptionID,ACCIDorCHARGETYPEID,IsChargeType
From tblQuotationDetails
where optionId=2


) a

on a.ACCIDorCHARGETYPEID=b.ACCIDorCHARGETYPEID and a.IsChargeType=b.IsChargeType
order by a.ID
