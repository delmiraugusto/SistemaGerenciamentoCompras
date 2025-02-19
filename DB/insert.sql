INSERT INTO Role (role) VALUES ('Administrador');
INSERT INTO Role (role) VALUES ('Cliente');

INSERT INTO User (email, password, name, roleID) VALUES 
('admin@example.com', '12345678', 'Administrador User', 1),
('customer1@example.com', '12345678', 'Cliente One', 2),
('customer2@example.com', '12345678', 'Cliente Two', 2);

INSERT INTO Product (name, price) VALUES 
('Laptop', 4500.00),
('Mouse', 150.00),
('Teclado', 300.00),
('Monitor', 1200.00),
('Cadeira Gamer', 850.00);

INSERT INTO Purchase (userID, total) VALUES 
(2, 4650.00), 
(3, 1350.00);

INSERT INTO PurchaseItem (purchaseID, productID, quantity, price) VALUES 
(1, 1, 1, 4500.00),
(1, 2, 1, 150.00),
(2, 3, 1, 300.00),
(2, 4, 1, 1200.00);



