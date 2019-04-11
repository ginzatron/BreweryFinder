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
	--imgSrc			nvarchar(500)		not null,

	constraint		pk_beer_id			primary key(id),
	constraint		fk_beers_style_id	foreign key(style_id) references styles(id)
)

CREATE TABLE breweries
(
	id				int					identity(1,1),
	name			nvarchar(100)		not null,
	happyHourFrom	Time		            null,
	happyHourTo  	Time		            null,
	established		int					not null,
	address			nvarchar(250)		not null,
	city			nvarchar(100)		not null,
	state			varchar(2)			not null,
	zip				int					not null,
	latitude        decimal(8,6)        not null,
	longitude		decimal(8,6)        not null,
	siteURL			nvarchar(100)		null,
	description		nvarchar(500)		not null,
	isBar			bit					not null,
	isBrewery       bit					not null

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

--Sibling Revelry
INSERT INTO beers (name, style_id, description, abv) VALUES ('Tangelo IPA', 1, 'Big pine flavor with aromas of citrus and stone fruits. Clean, dry finish', 6.6 );
INSERT INTO beers (name, style_id, description, abv) VALUES ('Cosmic Coaco', 2, 'Brewed with cocoa nibs, aged on oak spirals, spiced with Mexican vanilla for a nice milky finish', 6.6 );
INSERT INTO beers (name, style_id, description, abv) VALUES ('Pecan Brown Ale', 3, 'Full-bodied American, smooth brown ale brewed with candied pecans, brown sugar, and lactose.', 6.4);
INSERT INTO beers (name, style_id, description, abv) VALUES ('Irish Dry Stout', 2, 'Traditional, low gravity Irish stout brewed with barley flakes for added body. Roasted aroma with a little sweet malt. Finishes dry with chocolate undertones.', 4.2)

--Fatheads
INSERT INTO beers (name, style_id, description, abv) VALUES ('Head Hunter', 1, 'Head Hunter is an aggressively dry-hopped, West Coast-style IPA with a huge hop display of pine, grapefruit, citrus and pineapple. A brew for those who truly love their hops! Award-Winning.', 7.5)
INSERT INTO beers (name, style_id, description, abv) VALUES ('Bushwhacker Brown Ale', 3, 'The "Hefe" prefix means "with yeast" which gives this unfiltered wheat beer a somewhat cloudy or "foggy" appearance. Light aromas of wheat, bubblegum, clove and spice.', 5)
INSERT INTO beers (name, style_id, description, abv) VALUES ('Up In Smoke', 5, 'A robust porter brewed with malted barley and coffee. Smoked over Adler wood. Finishes nice and dry.', 9)

--Market Garden
INSERT INTO beers (name, style_id, description, abv) VALUES ('Progress', 4, 'Great American Beer Festival Award Winning Brew! Inspired by the Pilsners of the Rhine region, this lager is a tribute to the spicy, crisp and clean character of German noble hops and has just the right malt body to balance.', 4)
INSERT INTO beers (name, style_id, description, abv) VALUES ('Midnight Vorlauf', 5, 'This beer is brewed with brown, chocolate and crystalized malts then blended with rich and clean cold brewed coffee, yielding a smooth, robust character of a dark Porter.', 6)
INSERT INTO beers (name, style_id, description, abv) VALUES ('Particle Accelerator', 1, 'A bold floral and fruity hop profile courtesy of Centennial and Citra hops. Flaked rye and crystal malt provide a nice balance and round mouthfeel, obscuring a high ABV punch.', 9.2)

--GL
INSERT INTO beers (name, style_id, description, abv) VALUES ('Edmund Fitzgerald', 5, 'Brewed in memory of the sunken freighter with rich, roasted barley with bittersweet chocolate-coffee notes.', 6)
INSERT INTO beers (name, style_id, description, abv) VALUES ('Dortmunder', 6, 'A smooth, award-winning (and deceptively unassuming) balance of sweet malt and dry hop flavors. Smooth. Balanced. Charmingly unpretentious.' , 5.8)
INSERT INTO beers (name, style_id, description, abv) VALUES ('Chillwave', 1, 'Earthy, tropical Mosaic hops ride frontside here, followed by a deft cutback of sweet honey malt.', 9.1)
INSERT INTO beers (name, style_id, description, abv) VALUES ('Blackout Stout', 2, 'Pitch-dark and rich Imperial Stout. Kindled with black malt and roasted barley. Illuminated by flickers of bitter hops.', 9.9)

--Willoughby
INSERT INTO beers (name, style_id, description, abv) VALUES ('Rising Star Stout', 2, 'this stout has layers of chocolate, mocha and caramel all heightened by the heavy handed addition of locally roasted coffee.', 5.5)
INSERT INTO beers (name, style_id, description, abv) VALUES ('Community', 4, 'Simple, bready malts and a healthy dose of American and European Noble hops combine in a beer that truly anyone can enjoy.', 5)
INSERT INTO beers (name, style_id, description, abv) VALUES ('Nut Smasher', 2, 'Award-winning Imperial Stout. Seven types of malts. Infused with fresh-roasted coffee, chocolate and peanut butter. Rich, ebony pour with light brown head.', 11.5) 

INSERT INTO breweries (name, happyHourFrom, happyHourTo,established,address,city,state,zip,latitude,longitude,siteURL, description, isBar,isBrewery) Values ('Great Lakes Brewing', '04:00', '07:00', 1986, '2516 Market Ave', 'Cleveland', 'OH', 44113, 41.484417, -81.704427, 'www.greatlakesbrewing.com', 'Great Lakes Brewing Company is a brewery and brewpub in Cleveland, Ohio. The first brewpub and microbrewery in the state, Great Lakes Brewing has been noted as important to Cleveland''s local identity and as one of the initial forces behind the revival of the Ohio City neighborhood on Cleveland''s near West Side.', 1, 1) 
INSERT INTO breweries (name, happyHourFrom, happyHourTo,established,address,city,state,zip,latitude,longitude,siteURL,description,isBar,isBrewery) Values ('Platform Beer Company', null, null, 2014, '4125 Lorain Ave', 'Cleveland', 'OH', 44113, 41.479961, -81.714005, 'www.platformbeerco.com',   'Platform Beer Co. occupies a building that previously housed the Leisy Brewing Company (est. 1873). Platform produces beer for the Northeast Ohio market and is known for its wide variety and often avante-garde style offerings.',1,1)
INSERT INTO breweries  (name, happyHourFrom, happyHourTo,established,address,city,state,zip,latitude,longitude,siteURL,description,isBar,isBrewery) Values ('Willoughby Brewing Co.', '04:00', '07:00', 1995, '4057 Erie St.', 'Willoughby', 'OH', 44094, 41.641522, -81.405490, 'www.willoughbybrewing.com', 'Nestled in the heart of Historic Downtown Willoughby, Ohio lies Willoughby Brewing Co. Established in February of 1998, we are a full-service restaurant, bar and award-winning brewery in a 100-year-old building that was originally a rail-car repair depot for the Cleveland to Ashtabula Interurban Rail Line.',1,1)
INSERT INTO breweries  (name, happyHourFrom, happyHourTo,established,address,city,state,zip,latitude,longitude,siteURL,description,isBar,isBrewery) Values ('Sibling Revelry Brewery', null, null, 1995, '29305 Clemens Road', 'Westlake', 'OH', 44145, 41.470402, -81.946201, 'www.siblingrevelrybrewing.com', 'Sibling Revelry Brewing was founded by a family of brothers and cousins from Cleveland who wanted to share their passion for great beer with others. ... Pair beers with a rotating selection of food trucks with great foods like sandwiches, burgers, and more.',1,1)
INSERT INTO breweries  (name, happyHourFrom, happyHourTo,established,address,city,state,zip,latitude,longitude,siteURL,description,isBar,isBrewery) Values ('Market Garden Brewery', null, null, 2011, '1947 W 25TH ST', 'Cleveland', 'OH', 44113, 41.484896, -81.703650,'www.marketgardenbrewery.com', 'Market Garden Brewery,  located next door to the 100 year old West Side Market, has been stocked with a lineup of tasty, session beers like our award-winning Progress Pilsner, our organically hopped Citramax IPA, as well as Prosperity, our Bavarian style Hefeweizen, since 2011.',1,1)
INSERT INTO breweries  (name, happyHourFrom, happyHourTo,established,address,city,state,zip,latitude,longitude,siteURL,description,isBar,isBrewery) Values ('Fat Head''s Brewery', '02:00', '06:00', 2009, '17450 Engle Lake Dr', 'Middleburg Hts.', 'OH', 44143, 41.377386, -81.822942, 'www.fatheads.com', 'Brewmaster Matt Cole partnered with Glenn Benigni, owner of Fat Head''s Saloon in Pittsburgh, Pennsylvania, to open Fat Head''s Brewery & Saloon in Cleveland, Ohio in 2009. They grew to be known for their selection of craft beer and large sandwiches, called "headwiches."',1,1)

INSERT INTO beers_breweries Values (1, 11 )
INSERT INTO beers_breweries Values (1, 12 )
INSERT INTO beers_breweries Values (1, 13 )
INSERT INTO beers_breweries Values (1, 14 )
--INSERT INTO beers_breweries Values (2, 11 )
--INSERT INTO beers_breweries Values (2, 11 )
--INSERT INTO beers_breweries Values (2, 11 )
INSERT INTO beers_breweries Values (3, 15 )
INSERT INTO beers_breweries Values (3, 16 )
INSERT INTO beers_breweries Values (3, 17 )
INSERT INTO beers_breweries Values (4, 1 )
INSERT INTO beers_breweries Values (4, 2 )
INSERT INTO beers_breweries Values (4, 3 )
INSERT INTO beers_breweries Values (4, 4 )
INSERT INTO beers_breweries Values (5, 8 )
INSERT INTO beers_breweries Values (5, 9 )
INSERT INTO beers_breweries Values (5, 10 )
INSERT INTO beers_breweries Values (6, 5 )
INSERT INTO beers_breweries Values (6, 6 )
INSERT INTO beers_breweries Values (6, 7 )

COMMIT;


select * from beers
select * from beers_breweries
select * from breweries