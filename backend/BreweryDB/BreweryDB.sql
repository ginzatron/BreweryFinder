-- Switch to master
USE master;
GO

-- DROP the database if it exists
DROP DATABASE IF EXISTS BreweryDB;
GO

-- Create the db
CREATE DATABASE BreweryDB;
GO

-- Switch to the FlyInventory
USE BreweryDB;
GO

BEGIN TRANSACTION;

CREATE TABLE styles
(
	id				int					identity(1,1),
	name			nvarchar(100)		not null

	constraint		pk_styles_id		primary key(id)
)

CREATE TABLE beers
(
	id				int					identity(1,1),
	name			nvarchar(100)		not null,
	style_id		int					not null,
	description		nvarchar(150)		not null,
	abv				decimal(4,2)		not null,
	imgSrc		nvarchar(500)		not null,

	constraint		pk_beer_id			primary key(id),
	constraint		fk_beers_style_id	foreign key(style_id) references styles(id)
)

CREATE TABLE breweries
(
	id				int					identity(1,1),
	name			nvarchar(100)		not null,
	happyHour		nvarchar(50)		not null,
	operatingHours	nvarchar(50)		not null,
	established		int					not null,
	address			nvarchar(250)		not null,
	description		nvarchar(500)		not null,
	barOrRestaurant	varchar(20)			not null

	constraint		pk_brewery_id		primary key(id)
)

CREATE TABLE beers_breweries 
(
	brewery_id		int							not null,
	beer_id			int							not null

	constraint		pk_beers_breweries			primary key(brewery_id,beer_id)
	constraint		fk_beers_breweries_brewery	foreign key(brewery_id) references breweries(id),
	constraint		fk_beers_breweries_beer		foreign key(beer_id) references beers(id)
)

COMMIT;