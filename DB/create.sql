CREATE TABLE IF NOT EXISTS Role ( id INTEGER PRIMARY KEY AUTOINCREMENT, role TEXT NOT NULL)

CREATE TABLE IF NOT EXISTS User ( id INTEGER PRIMARY KEY AUTOINCREMENT, email TEXT NOT NULL, password TEXT NOT NULL, name TEXT NOT NULL, roleID INTEGER, FOREIGN KEY (roleID) REFERENCES Role(id));

CREATE TABLE IF NOT EXISTS Product ( id INTEGER PRIMARY KEY AUTOINCREMENT, name TEXT NOT NULL, price DECIMAL(10, 2) NOT NULL );

CREATE TABLE IF NOT EXISTS Purchase ( id INTEGER PRIMARY KEY AUTOINCREMENT, userID INTEGER, productID INTEGER, orderDate DATETIME, total DECIMAL(10, 2) NOT NULL, FOREIGN KEY (userID) REFERENCES User(id), FOREIGN KEY (productID) REFERENCES Product(id));

