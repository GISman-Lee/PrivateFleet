-- Defualt Values of ACU_UserMaster
INSERT INTO [dbo].[ACU_UserMaster]([Username],[Password],[Name],[Email],[Phone],[Address],[IsActive]) VALUES('admin','admin','Private Fleet Admin','maheshg@mechsoftgroup.com','','',1)

-- Defualt Values of role master

INSERT INTO ACU_RoleMaster values('Admin','Administrator of private fleet',1)
INSERT INTO ACU_RoleMaster values('Dealer','',1)
INSERT INTO ACU_RoleMaster values('Consultant','',1)



--Deafualt Values Of tbl_ConfigValues

INSERT INTO tbl_ConfigValues VALUES('NO_OF_DAYS_TO_RECEIVE_QUOTATION',12,'No of days within which the consultant should recieve the quotation',1)
INSERT INTO tbl_ConfigValues VALUES('NO_OF_DEALER_FOR_REQUEST',5,'No of dealers to which one quote request can be send as enquiry',1)
INSERT INTO tbl_ConfigValues VALUES('NO_OF_HOT_DEALERS',0,'No of hot dealers to select while sending the quote request',1)
INSERT INTO tbl_ConfigValues VALUES('NO_OF_NORMAL_DEALERS',1,'No Of normal dealers that must be selected',1)
INSERT INTO tbl_ConfigValues VALUES('NO_OF_DAYS_TO_REDUCE_POINT',5,'No. of days after which the points of the deal will be started to deduct',1)
INSERT INTO tbl_ConfigValues VALUES('No_OF_OPTIONS_IN_QUOTATION',3,'No of Options available  to give while creating quotation',1)
INSERT INTO tbl_ConfigValues VALUES('CHECK_DEALER_LIMIT',1,'To check dealer selection limit or ''Not (0-> ''No, 1-> Yes)',1)
INSERT INTO tbl_ConfigValues VALUES('No_OF_POINTS_AFTER_SHORTLISTING',5,'No Of points to give to the short listed quotations dealer',1)
INSERT INTO tbl_ConfigValues VALUES('No_OF_POINTS_TO_REDUCE',2,'No of points to reduce from dealer account depending on the  ''No of days elapsed from the date quotation is short listed',1)
INSERT INTO tbl_ConfigValues VALUES('No_OF_DAYS_TO_REDUCE_POINTS_AFTER_SHORTLISTING',6,'days after which the points will be reduced from the dealers account due to aging of the deal',1)
INSERT INTO tbl_ConfigValues VALUES('NO_OF_POINTS_AFTER_DEAL_DONE',10,'No Of Points to credit in dealers account after deal is done',1)

--Deafualt Values Of  ACU_ActionMaster

INSERT INTO ACU_ActionMaster VALUES('Add','',1)
INSERT INTO ACU_ActionMaster VALUES('Edit','',1)
INSERT INTO ACU_ActionMaster VALUES('ViewDetails','To show details related to an entity',1)
INSERT INTO ACU_ActionMaster VALUES('Activeness','',1)
INSERT INTO ACU_ActionMaster VALUES('View','default action to view each page',1)


--Deafualt Values Of ACU_AccessTypeMaster
INSERT INTO ACU_AccessTypeMaster VALUES('Role',0,NULL,1)
INSERT INTO ACU_AccessTypeMaster VALUES('User',1,NULL,1)


--Deafualt Values Of ACU_PageMaster

INSERT INTO ACU_PageMaster VALUES('Role Master','RoleMaster.aspx',	5	,	0	,	1	)
INSERT INTO ACU_PageMaster VALUES('View Quotation Details','ViewQuotation.aspx',	35	,	1	,	1	)
INSERT INTO ACU_PageMaster VALUES('Make Master','MakeMaster.aspx',	5	,	0	,	1	)
INSERT INTO ACU_PageMaster VALUES('Dealer Master','DealerMaster.aspx',	5	,	0	,	1	)
INSERT INTO ACU_PageMaster VALUES('Masters','#',	0	,	0	,	1	)
INSERT INTO ACU_PageMaster VALUES('Mappings','#',	0	,	0	,	1	)
INSERT INTO ACU_PageMaster VALUES('Make-Dealer','MakeDealerMaster.aspx',	6	,	0	,	1	)
INSERT INTO ACU_PageMaster VALUES('Series-Accessories','SeriesAccessoriesMaster.aspx',	6	,	0	,	1	)
INSERT INTO ACU_PageMaster VALUES('Role-Access','RoleAccess.aspx',	6	,	0	,	1	)
INSERT INTO ACU_PageMaster VALUES('Accessory Master','AccessoriesMaster.aspx',	5	,	0	,	1	)
INSERT INTO ACU_PageMaster VALUES('Fixed Charges','ChargeTypeMaster.aspx',	5	,	0	,	1	)
INSERT INTO ACU_PageMaster VALUES('City Master','CityMaster.aspx',	5	,	0	,	1	)
INSERT INTO ACU_PageMaster VALUES('Configuration Values','AddConfigValues.aspx',	5	,	0	,	1	)
INSERT INTO ACU_PageMaster VALUES('Location Master','LocationMaster.aspx',	5	,	0	,	1	)
INSERT INTO ACU_PageMaster VALUES('Model Master','ModelMaster.aspx',	5	,	0	,	1	)
INSERT INTO ACU_PageMaster VALUES('Series Master','SeriesMaster.aspx',	5	,	0	,	1	)
INSERT INTO ACU_PageMaster VALUES('State Master','StateMaster.aspx',	5	,	0	,	1	)
INSERT INTO ACU_PageMaster VALUES('User Master','UserMaster.aspx',	5	,	0	,	1	)
INSERT INTO ACU_PageMaster VALUES('Page Master','ManagePages.aspx',	5	,	0	,	1	)
INSERT INTO ACU_PageMaster VALUES('Action Master','ManageActions.aspx',	5	,	0	,	1	)
INSERT INTO ACU_PageMaster VALUES('Page-Action','PageActionMapping.aspx',	6	,	0	,	1	)
INSERT INTO ACU_PageMaster VALUES('Quote Request','#',	0	,	0	,	1	)
INSERT INTO ACU_PageMaster VALUES('Create Quote Request','QuoteRequest.aspx',	22	,	0	,	1	)
INSERT INTO ACU_PageMaster VALUES('View Sent Requests','ViewSentRequests.aspx',	22	,	0	,	1	)
INSERT INTO ACU_PageMaster VALUES('Quotation','#',	0	,	0	,	1	)
INSERT INTO ACU_PageMaster VALUES('View Received Requests','ViewRecivedQuoteRequests.aspx',	25	,	0	,	1	)
INSERT INTO ACU_PageMaster VALUES('View Sent Request Details','ViewSentRequestDetails.aspx',	35	,	1	,	1	)
INSERT INTO ACU_PageMaster VALUES('Hot Dealers Selection','HotDealers.aspx',	6	,	0	,	1	)
INSERT INTO ACU_PageMaster VALUES('View Quotations','ViewDealersQuotation.aspx',	25	,	0	,	1	)
INSERT INTO ACU_PageMaster VALUES('Create Quotation','Quotation.aspx',	35	,	1	,	1	)
INSERT INTO ACU_PageMaster VALUES('Reports','#',	0	,	0	,	1	)
INSERT INTO ACU_PageMaster VALUES('Quotation Report','AdminReport.aspx',	31	,	0	,	1	)
INSERT INTO ACU_PageMaster VALUES('Compare Quotations','QuoteComparison.aspx',	35	,	1	,	1	)
INSERT INTO ACU_PageMaster VALUES('View Shortlisted Quotation','ViewShortListedQuotation.aspx',	35	,	1	,	1	)
INSERT INTO ACU_PageMaster VALUES('Internal Links','#',	0	,	1	,	1	)



--Deafualt Values Of tblChargesTypesMaster

INSERT INTO tblChargesTypesMaster VALUES('Pre-Delivery',2,1	)
INSERT INTO tblChargesTypesMaster VALUES('Fleet Discount',3,1	)
INSERT INTO tblChargesTypesMaster VALUES('LCT/GST',4,1)
INSERT INTO tblChargesTypesMaster VALUES('Stamp Duty',5,1)
INSERT INTO tblChargesTypesMaster VALUES('Registration',6,1	)
INSERT INTO tblChargesTypesMaster VALUES('Plate Fee',	7	,1	)
INSERT INTO tblChargesTypesMaster VALUES('CTP',	8	,1	)
INSERT INTO tblChargesTypesMaster VALUES('Total-On Road Cost (Inclusive of GST)',	9	,1	)
INSERT INTO tblChargesTypesMaster VALUES('Recommended Retail Price Exc GST',	1	,1	)



--Deafualt Values Of tblAccessoriesMaster


INSERT INTO tblAccessoriesMaster VALUES('Body Type',	1	,	1	)
INSERT INTO tblAccessoriesMaster VALUES('Color',	1	,	1	)
INSERT INTO tblAccessoriesMaster VALUES('Transmission',	1	,	1	)

