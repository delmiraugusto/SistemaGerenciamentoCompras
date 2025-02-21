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
(1, 1200.00),
(3, 750.00),  
(2, 6700.00), 
(1, 3200.00), 
(7, 8000.00), 
(8, 2550.00), 
(9, 5000.00), 
(7, 1350.00), 
(10, 2100.00), 
(4, 3650.00); 
(5, 4000.00),  
(2, 5600.00), 
(8, 1400.00), 
(9, 3000.00),  
(6, 1600.00),  
(10, 3600.00), 
(1, 2800.00), 
(3, 9000.00),  
(4, 2700.00),  
(7, 1000.00),  
(8, 7500.00), 
(2, 1600.00), 
(9, 600.00),   
(6, 4300.00), 
(10, 500.00), 
(5, 4700.00),  
(3, 1200.00), 
(1, 7500.00),  
(4, 5000.00), 
(7, 300.00);   


INSERT INTO PurchaseItem (purchaseID, productID, quantity) VALUES 
(3, 9, 3),  
(4, 7, 3), 
(5, 1, 1),  
(5, 6, 2),  
(6, 3, 4), 
(6, 5, 2), 
(7, 10, 1), 
(8, 5, 2), 
(8, 8, 5),  
(9, 4, 2),  
(10, 2, 4), 
(10, 2, 3),
(11, 6, 3), 
(12, 4, 4), 
(12, 8, 3); 
(13, 4, 1), (13, 5, 1),
(14, 10, 1),
(15, 6, 2),
(16, 3, 3), (16, 8, 2),
(17, 7, 4), (17, 2, 2),
(18, 1, 4), (18, 4, 1),
(19, 9, 7),
(20, 10, 1), (20, 8, 5),
(21, 5, 1), (21, 3, 1), (21, 8, 4),
(22, 1, 2), (22, 2, 1),
(23, 10, 1),
(24, 4, 1),
(25, 6, 1),
(26, 3, 5),
(27, 8, 5),
(28, 1, 10), (28, 2, 1),
(29, 7, 4), (29, 8, 2),
(30, 10, 1),
(31, 4, 2),
(32, 8, 3);


-- O Update é porque o total no "Purchase", quando inserido vem com a data atual(DateTime.now).
-- Ai o update é para melhor visualização no PowerBI.

UPDATE Purchase SET orderDate = CASE 
    WHEN userID = 5 THEN '2024-12-15 14:30:00'
    WHEN userID = 2 THEN '2025-01-10 10:15:23'
    WHEN userID = 8 THEN '2024-12-20 09:45:56'
    WHEN userID = 9 THEN '2025-02-05 18:20:10'
    WHEN userID = 6 THEN '2025-01-22 12:05:47'
    WHEN userID = 10 THEN '2025-02-18 16:40:30'
    WHEN userID = 1 THEN '2024-12-05 08:10:15'
    WHEN userID = 3 THEN '2025-02-09 20:25:33'
    WHEN userID = 4 THEN '2025-01-15 15:55:12'
    WHEN userID = 7 THEN '2025-01-02 11:45:50'
    WHEN userID = 8 THEN '2025-01-29 14:30:21'
    WHEN userID = 2 THEN '2025-01-19 09:35:44'
    WHEN userID = 9 THEN '2025-02-13 17:05:38'
    WHEN userID = 6 THEN '2025-01-25 21:55:17'
    WHEN userID = 10 THEN '2025-02-07 10:20:59'
    WHEN userID = 5 THEN '2024-12-12 13:50:33'
    WHEN userID = 3 THEN '2025-02-14 08:40:27'
    WHEN userID = 1 THEN '2025-01-31 19:15:48'
    WHEN userID = 4 THEN '2025-02-10 12:35:52'
    WHEN userID = 7 THEN '2024-12-09 07:55:22'
END;



