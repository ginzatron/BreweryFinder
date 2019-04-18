--delete data from tables
delete from beers_breweries;
delete from favorites;
delete from beers;
delete from breweries;
delete from styles;

--insert a style
SET IDENTITY_INSERT styles ON;
INSERT INTO styles (id,name) VALUES (1,'IPA');
SET IDENTITY_INSERT styles OFF;

--insert a beer
INSERT INTO beers (name, style_id, description, abv) VALUES ('Head Hunter', 1, 'Head Hunter is an aggressively dry-hopped, West Coast-style IPA with a huge hop display of pine, grapefruit, citrus and pineapple. A brew for those who truly love their hops! Award-Winning.', 7.5)
declare @test_beer_id int = (select @@identity);

--insert a brewery
INSERT INTO breweries  (name, happyHourFrom, happyHourTo,established,address,city,state,zip,latitude,longitude,siteURL,description,isBar,isBrewery) Values ('Fat Head''s Brewery', '02:00', '06:00', 2009, '17450 Engle Lake Dr', 'Middleburg Hts.', 'OH', 44143, 41.377386, -81.822942, 'www.fatheads.com', 'Brewmaster Matt Cole partnered with Glenn Benigni, owner of Fat Head''s Saloon in Pittsburgh, Pennsylvania, to open Fat Head''s Brewery & Saloon in Cleveland, Ohio in 2009. They grew to be known for their selection of craft beer and large sandwiches, called "headwiches."',1,1)
declare @test_brew_id int = (select @@identity);

--insert the beer/brewery combo
INSERT INTO beers_breweries Values (@test_brew_id, @test_beer_id )
