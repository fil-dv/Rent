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
RentAreaAddressRegion = 'Вінницька',
RentAreaAddressCity = 'м. Вінниця',
RentAreaAddressStreet = 'вул. Хмельницьке Шосе буд. 20 кв. 5',
LegalAddressRegion = RentAreaAddressRegion,
LegalAddressCity = RentAreaAddressCity, 
LegalAddressStreet = RentAreaAddressStreet

where AreaID = 598;
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

update Areas set 
LegalAddressRegion = RentAreaAddressRegion,
RentAreaAddressRegion = LegalAddressRegion,
LegalAddressCity = RentAreaAddressCity, 
RentAreaAddressCity = LegalAddressCity,
LegalAddressStreet = RentAreaAddressStreet,
RentAreaAddressStreet = LegalAddressStreet;
GO

update Areas set AreaDescription = 'Склад общей площадью '+ Convert(NVARCHAR, Areas.SquareArea) +' квадратных метров расположен в отдельно стоящем современном здании (корпусе), входящем в логистический комплекс "Технопром". Заезд осуществляется непосредственно с Новорязанского шоссе. В перечень наших услуг входят не только традиционные ответственное хранение, приемка товаров от поставщиков и размещение на местах хранения, но и комплектация отгрузок по заявкам клиентов, подготовка сопроводительных документов грузоперевозок, специальная упаковка грузов, их маркировка, проведение инвентаризаций и многое другое. Мы работаем с такими сложными товарами как запасные части для транспортных средств, товары парфюмерной промышленности, вычислительной и бытовой техники, с изделиями в стеклянной таре.' where AreaTypeID = 1

GO

update Areas set AreaDescription = 'Гараж в плане имеет форму, напоминающую гигантскую подкову. Столь необычная планировка гаража была выбрана Мельниковым из-за неудобной треугольной формы выделенного под застройку участка. Разработанная архитектором подковообразная схема парковки машин обеспечивала компактное размещение максимально возможного количества грузовиков на данной площади и их удобный въезд и выезд из гаража.' where AreaTypeID = 2
GO

update Areas set AreaDescription = 'Офис расположен в центре '+ Areas.RentAreaAddressCity +'. Фасад здания выходит непосредственно на улицу – ключевое направление к деловому и административному центру. Такое местоположение обеспечивает объекту удобную транспортную доступность ко всем основным деловым районам города.' where AreaTypeID = 3
GO

update Areas set RentAreaAddressCity = 'м. Ніжин ' where RentAreaAddressCity = 'Приватне підприємство ”СВВ ПЛЮС””'
GO






