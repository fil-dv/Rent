update Areas set  ContactaName = OwnerName
where ContactaName is null;

update Areas set Areas.AreaDescription = 'В хорошем состоянии'
where AreaID % 2 = 0;

update Areas set Areas.AreaTypeID = 2
where AreaID % 2 = 0;

update Areas set Areas.AreaTypeID = 3
where AreaID % 3 = 0;

