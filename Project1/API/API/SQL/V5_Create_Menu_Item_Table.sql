﻿CREATE TABLE [Menu_Item](
    ITEM_ID UNIQUEIDENTIFIER NOT NULL,
    MENU_ID UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY (MENU_ID, ITEM_ID),
    FOREIGN KEY (ITEM_ID) 
        REFERENCES [ITEM] (ID),
    FOREIGN KEY (MENU_ID)
        REFERENCES [MENU] (ID)
);