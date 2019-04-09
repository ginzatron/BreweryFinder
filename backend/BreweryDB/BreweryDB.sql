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
	description		nvarchar(400)		not null,
	abv				decimal(4,2)		not null,
	imgSrc			nvarchar(500)		not null,

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

SET IDENTITY_INSERT styles ON;

INSERT INTO styles (id,name) VALUES (1,'IPA');
INSERT INTO styles (id,name) VALUES (2,'Stout');
INSERT INTO styles (id,name) VALUES (3,'Brown Ale');
INSERT INTO styles (id,name) VALUES (4,'Pilsner')
INSERT INTO styles (id,name) VALUES (5,'Porter')
INSERT INTO styles (id,name) VALUES (6,'Dortmunder')

SET IDENTITY_INSERT styles OFF;

SET IDENTITY_INSERT beers ON;

INSERT INTO beers (id,name, style_id, description, abv, imgSrc) VALUES (1,'Cosmic Coaco',2,'Brewed with cocoa nibs, aged on oak spirals, spiced with Mexican vanilla for a nice milky finish', 6.6,'http://later');
INSERT INTO beers (id,name, style_id, description, abv, imgSrc) VALUES (2,'Pecan Brown Ale',3,'Full-bodied American, smooth brown ale brewed with candied pecans, brown sugar, and lactose.', 6.4,'http://later');
INSERT INTO beers (id,name, style_id, description, abv, imgSrc) VALUES (3,'Irish Dry Stout',2,'Traditional, low gravity Irish stout brewed with barley flakes for added body. Roasted aroma with a little sweet malt. Finishes dry with chocolate undertones.', 4.2,'http://later')

SET IDENTITY_INSERT beers OFF;



COMMIT;