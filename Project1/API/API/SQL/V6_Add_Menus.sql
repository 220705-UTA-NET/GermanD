BEGIN TRANSACTION;

declare @FoodMenuID uniqueidentifier
set @FoodMenuID = NEWID()

declare @DrinkMenuID uniqueidentifier
set @DrinkMenuID = NEWID()

INSERT INTO [Menu](ID, NAME) VALUES(@FoodMenuID, 'Food Menu');
INSERT INTO [Menu](ID, NAME) VALUES(@DrinkMenuID, 'Drink Menu');

DECLARE @HamburgerID UNIQUEIDENTIFIER;
DECLARE @CocaColaID UNIQUEIDENTIFIER;

SELECT @HamburgerID = ID FROM dbo.[Item] WHERE NAME='Hamburger';
SELECT @CocaColaID = ID FROM dbo.[Item] WHERE NAME='Coca Cola';

INSERT INTO [Menu_Item](MENU_ID, ITEM_ID) 
    VALUES(@FoodMenuID, @HamburgerID), (@DrinkMenuID, @CocaColaID);

COMMIT TRANSACTION;