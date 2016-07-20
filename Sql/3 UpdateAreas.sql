USE [Landlord]
GO

update Areas set  ContactaName = OwnerName
where ContactaName is null;
GO

update Areas set  
LegalAddressRegion = RentAreaAddressRegion,
LegalAddressCity = RentAreaAddressCity, 
LegalAddressStreet = RentAreaAddressStreet

where LegalAddressStreet is null;
GO

update Areas set 
RentAreaAddressRegion = '³�������',
RentAreaAddressCity = '�. ³�����',
RentAreaAddressStreet = '���. ����������� ���� ���. 20 ��. 5',
LegalAddressRegion = RentAreaAddressRegion,
LegalAddressCity = RentAreaAddressCity, 
LegalAddressStreet = RentAreaAddressStreet

where AreaID = 598;
GO

update Areas set Areas.AreaDescription = '� ������� ���������'
where AreaID % 2 = 0;
GO

update Areas set Areas.AreaTypeID = 2
where AreaID % 2 = 0;
GO

update Areas set Areas.AreaTypeID = 3
where AreaID % 3 = 0;
GO

update Areas set 
LegalAddressRegion = RentAreaAddressRegion,
RentAreaAddressRegion = LegalAddressRegion,
LegalAddressCity = RentAreaAddressCity, 
RentAreaAddressCity = LegalAddressCity,
LegalAddressStreet = RentAreaAddressStreet,
RentAreaAddressStreet = LegalAddressStreet;
GO
