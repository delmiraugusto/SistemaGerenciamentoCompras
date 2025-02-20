INSERT INTO Role (role) VALUES ('Administrador');
INSERT INTO Role (role) VALUES ('Cliente');

INSERT INTO User (email, password, name, roleID) VALUES 
('user1@example.com', '12345678', 'Cliente User 1', 1),
('user2@example.com', '12345678', 'Cliente User 2', 2),
('user3@example.com', '12345678', 'Cliente User 3', 2),
('user4@example.com', '12345678', 'Cliente User 4', 2),
('user5@example.com', '12345678', 'Cliente User 5', 2),
('user6@example.com', '12345678', 'Cliente User 6', 2),
('user7@example.com', '12345678', 'Cliente User 7', 2),
('user8@example.com', '12345678', 'Cliente User 8', 2),
('user9@example.com', '12345678', 'Cliente User 9', 2),
('user10@example.com', '12345678', 'Cliente User 10', 2);

INSERT INTO Product (name, price) VALUES 
('Headset Gamer', 350.00),
('Webcam', 200.00),
('Impressora', 850.00),
('Smartphone', 2500.00),
('Tablet', 1500.00),
('Smartwatch', 700.00),
('Fone Bluetooth', 250.00),
('Cabo HDMI', 80.00),
('HD Externo', 400.00),
('Notebook Gamer', 8000.00);

INSERT INTO Purchase (userID, total) VALUES 
(3, 1200.00), -- HD Externo (3 unidades x 400.00)
(4, 750.00),  -- Fone Bluetooth (3 unidades x 250.00)
(5, 6700.00), -- Laptop (1 unidade x 4500.00) + Headset Gamer (2 unidades x 350.00)
(6, 3200.00), -- Teclado (4 unidades x 300.00) + Tablet (2 unidades x 1500.00)
(7, 8000.00), -- Notebook Gamer (1 unidade x 8000.00)
(8, 2550.00), -- Cadeira Gamer (2 unidades x 850.00) + Cabo HDMI (5 unidades x 80.00)
(9, 5000.00), -- Smartphone (2 unidades x 2500.00)
(10, 1350.00), -- Webcam (4 unidades x 200.00) + Mouse (3 unidades x 150.00)
(11, 2100.00), -- Smartwatch (3 unidades x 700.00)
(12, 3650.00); -- Impressora (4 unidades x 850.00) + Cabo HDMI (3 unidades x 80.00)

INSERT INTO PurchaseItem (purchaseID, productID, quantity) VALUES 
(3, 9, 3),  -- Compra de HD Externo (3 unidades)
(4, 7, 3),  -- Compra de Fone Bluetooth (3 unidades)
(5, 1, 1),  -- Laptop (1 unidade)
(5, 6, 2),  -- Headset Gamer (2 unidades)
(6, 3, 4),  -- Teclado (4 unidades)
(6, 5, 2),  -- Tablet (2 unidades)
(7, 10, 1), -- Notebook Gamer (1 unidade)
(8, 5, 2),  -- Cadeira Gamer (2 unidades)
(8, 8, 5),  -- Cabo HDMI (5 unidades)
(9, 4, 2),  -- Smartphone (2 unidades)
(10, 2, 4), -- Webcam (4 unidades)
(10, 2, 3), -- Mouse (3 unidades)
(11, 6, 3), -- Smartwatch (3 unidades)
(12, 4, 4), -- Impressora (4 unidades)
(12, 8, 3); -- Cabo HDMI (3 unidades)
