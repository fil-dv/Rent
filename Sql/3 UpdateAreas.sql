USE [Landlord]
GO

update Areas set  ContactaName = OwnerName
where ContactaName is null;
GO

update Areas set Areas.AreaDescription = 'В хорошем состоянии'
where AreaID % 2 = 0;
GO

update Areas set Areas.AreaTypeID = 2
where AreaID % 2 = 0;
GO

update Areas set Areas.AreaTypeID = 3
where AreaID % 3 = 0;
GO
