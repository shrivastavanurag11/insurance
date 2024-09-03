INSERT INTO Users (UserName, Password, UserType, FirstName, LastName, Age, Gender, Email, ContactNo, Address, CustomerImage)
VALUES 
('user1', '123', 'Customer', 'John', 'Doe', 30, 'M', 'user1@example.com', '1234567890', 'Address 1', NULL),
('user2', '123', 'Customer', 'Jane', 'Doe', 25, 'F', 'user2@example.com', '1234567891', 'Address 2', NULL),
('user3', '123', 'Customer', 'Alice', 'Smith', 28, 'F', 'user3@example.com', '1234567892', 'Address 3', NULL),
('user4', '123', 'Customer', 'Bob', 'Johnson', 35, 'M', 'user4@example.com', '1234567893', 'Address 4', NULL),
('user5', '123', 'Customer', 'Charlie', 'Brown', 40, 'M', 'user5@example.com', '1234567894', 'Address 5', NULL),
('user6', '123', 'Customer', 'David', 'White', 22, 'M', 'user6@example.com', '1234567895', 'Address 6', NULL),
('user7', '123', 'Customer', 'Eve', 'Black', 29, 'F', 'user7@example.com', '1234567896', 'Address 7', NULL),
('user8', '123', 'Customer', 'Frank', 'Green', 45, 'M', 'user8@example.com', '1234567897', 'Address 8', NULL),
('user9', '123', 'Customer', 'Grace', 'Blue', 32, 'F', 'user9@example.com', '1234567898', 'Address 9', NULL),
('user10', '123', 'Customer', 'Henry', 'Yellow', 37, 'M', 'user10@example.com', '1234567899', 'Address 10', NULL),
('user11', '123', 'Customer', 'Isabella', 'Red', 31, 'F', 'user11@example.com', '1234567800', 'Address 11', NULL),
('user12', '123', 'Customer', 'Jack', 'Purple', 42, 'M', 'user12@example.com', '1234567801', 'Address 12', NULL),
('user13', '123', 'Customer', 'Katherine', 'Orange', 26, 'F', 'user13@example.com', '1234567802', 'Address 13', NULL),
('user14', '123', 'Customer', 'Liam', 'Pink', 38, 'M', 'user14@example.com', '1234567803', 'Address 14', NULL),
('user15', '123', 'Customer', 'Mia', 'Gray', 33, 'F', 'user15@example.com', '1234567804', 'Address 15', NULL),
('user16', '123', 'Customer', 'Noah', 'White', 36, 'M', 'user16@example.com', '1234567805', 'Address 16', NULL),
('user17', '123', 'Customer', 'Olivia', 'Black', 27, 'F', 'user17@example.com', '1234567806', 'Address 17', NULL),
('user18', '123', 'Customer', 'Paul', 'Green', 34, 'M', 'user18@example.com', '1234567807', 'Address 18', NULL),
('user19', '123', 'Customer', 'Quinn', 'Blue', 39, 'M', 'user19@example.com', '1234567808', 'Address 19', NULL),
('user20', '123', 'Customer', 'Rachel', 'Brown', 28, 'F', 'user20@example.com', '1234567809', 'Address 20', NULL);



----------------------------
INSERT INTO Policies (PolicyID, PolicyType, PolicyName, InsuranceAmount, PolicyValidity, PolicyDescription, Available)
VALUES 
(1, 'Life', 'Life Insurance Basic', 100000.00, 20, 'Basic life insurance policy.', 'Y'),
(2, 'Life', 'Life Insurance Premium', 200000.00, 25, 'Premium life insurance policy.', 'Y'),
(3, 'Health', 'Health Insurance Basic', 50000.00, 15, 'Basic health insurance policy.', 'Y'),
(4, 'Health', 'Health Insurance Premium', 150000.00, 20, 'Premium health insurance policy.', 'Y'),
(5, 'Car', 'Car Insurance Basic', 30000.00, 5, 'Basic car insurance policy.', 'Y'),
(6, 'Car', 'Car Insurance Premium', 100000.00, 10, 'Premium car insurance policy.', 'Y'),
(7, 'Home', 'Home Insurance Basic', 250000.00, 30, 'Basic home insurance policy.', 'Y'),
(8, 'Home', 'Home Insurance Premium', 500000.00, 35, 'Premium home insurance policy.', 'Y'),
(9, 'Travel', 'Travel Insurance Basic', 20000.00, 2, 'Basic travel insurance policy.', 'Y'),
(10, 'Travel', 'Travel Insurance Premium', 50000.00, 5, 'Premium travel insurance policy.', 'Y'),
(11, 'Life', 'Life Insurance Plus', 150000.00, 22, 'Enhanced life insurance policy.', 'Y'),
(12, 'Health', 'Health Insurance Plus', 80000.00, 18, 'Enhanced health insurance policy.', 'Y'),
(13, 'Car', 'Car Insurance Plus', 70000.00, 8, 'Enhanced car insurance policy.', 'Y'),
(14, 'Home', 'Home Insurance Plus', 350000.00, 33, 'Enhanced home insurance policy.', 'Y'),
(15, 'Travel', 'Travel Insurance Plus', 40000.00, 3, 'Enhanced travel insurance policy.', 'Y'),
(16, 'Life', 'Life Insurance Ultimate', 250000.00, 30, 'Ultimate life insurance policy.', 'Y'),
(17, 'Health', 'Health Insurance Ultimate', 100000.00, 25, 'Ultimate health insurance policy.', 'Y'),
(18, 'Car', 'Car Insurance Ultimate', 120000.00, 12, 'Ultimate car insurance policy.', 'Y'),
(19, 'Home', 'Home Insurance Ultimate', 600000.00, 40, 'Ultimate home insurance policy.', 'Y'),
(20, 'Travel', 'Travel Insurance Ultimate', 75000.00, 7, 'Ultimate travel insurance policy.', 'Y');



-------------------------

INSERT INTO PolicySold (UserID, PolicyID, SoldDate, Amount)
VALUES 
(1, 1, '2024-01-01', 100000.00),
(2, 2, '2024-02-01', 200000.00),
(3, 3, '2024-03-01', 50000.00),
(4, 4, '2024-04-01', 150000.00),
(5, 5, '2024-05-01', 30000.00),
(6, 6, '2024-06-01', 100000.00),
(7, 7, '2024-07-01', 250000.00),
(8, 8, '2024-08-01', 500000.00),
(9, 9, '2024-09-01', 20000.00),
(10, 10, '2024-10-01', 50000.00);




------------------
INSERT INTO Claims (PurchaseId, ClaimAmount, remainingAmount, ClaimDate)
VALUES 
(1, 10000.00, 90000.00, '2024-07-01'),
(2, 50000.00, 150000.00, '2024-08-01'),
(3, 10000.00, 40000.00, '2024-09-01'),
(4, 75000.00, 75000.00, '2024-10-01'),
(5, 5000.00, 25000.00, '2024-11-01');
