CREATE TABLE IF NOT EXISTS Role (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    role TEXT NOT NULL
);

CREATE TABLE IF NOT EXISTS User (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    email TEXT NOT NULL,
    password TEXT NOT NULL,
    name TEXT NOT NULL,
    roleID INTEGER,
    FOREIGN KEY (roleID) REFERENCES Role(id)
);

CREATE TABLE IF NOT EXISTS Product (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    name TEXT NOT NULL,
    price DECIMAL(10, 2) NOT NULL
);

CREATE TABLE IF NOT EXISTS Purchase (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    userID INTEGER,
    orderDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    total DECIMAL(10, 2),
    FOREIGN KEY (userID) REFERENCES User(id)
);

CREATE TABLE IF NOT EXISTS PurchaseItem (
    id INTEGER PRIMARY KEY AUTOINCREMENT,
    purchaseID INTEGER NOT NULL,
    productID INTEGER NOT NULL,
    quantity INTEGER NOT NULL,
    FOREIGN KEY (purchaseID) REFERENCES Purchase(id) ON DELETE CASCADE,
    FOREIGN KEY (productID) REFERENCES Product(id) ON DELETE CASCADE
);
