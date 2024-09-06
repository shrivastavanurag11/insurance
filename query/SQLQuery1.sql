--fake data forn users
INSERT INTO Users (UserName, Password, UserType, FirstName, LastName, Age, Gender, Email, ContactNo, Address, CustomerImage)
VALUES
('john_doe', '123', 'Customer', 'John', 'Doe', 28, 'M', 'john.doe@example.com', '+1234567890', '123 Main St, Springfield, USA', NULL),
('jane_smith', '123', 'Customer', 'Jane', 'Smith', 35, 'F', 'jane.smith@example.com', '+1234567891', '456 Oak St, Springfield, USA', NULL),
('michael_brown', '123', 'Customer', 'Michael', 'Brown', 42, 'M', 'michael.brown@example.com', '+1234567892', '789 Pine St, Springfield, USA', NULL),
('linda_johnson', '123', 'Customer', 'Linda', 'Johnson', 29, 'F', 'linda.johnson@example.com', '+1234567893', '321 Elm St, Springfield, USA', NULL),
('robert_miller', '123', 'Customer', 'Robert', 'Miller', 34, 'M', 'robert.miller@example.com', '+1234567894', '654 Cedar St, Springfield, USA', NULL),
('patricia_davis', '123', 'Customer', 'Patricia', 'Davis', 31, 'F', 'patricia.davis@example.com', '+1234567895', '987 Maple St, Springfield, USA', NULL),
('david_wilson', '123', 'Customer', 'David', 'Wilson', 26, 'M', 'david.wilson@example.com', '+1234567896', '159 Birch St, Springfield, USA', NULL),
('barbara_moore', '123', 'Customer', 'Barbara', 'Moore', 39, 'F', 'barbara.moore@example.com', '+1234567897', '753 Oak St, Springfield, USA', NULL),
('james_taylor', '123', 'Customer', 'James', 'Taylor', 33, 'M', 'james.taylor@example.com', '+1234567898', '951 Pine St, Springfield, USA', NULL),
('mary_anderson', '123', 'Customer', 'Mary', 'Anderson', 45, 'F', 'mary.anderson@example.com', '+1234567899', '852 Elm St, Springfield, USA', NULL),
('william_white', '123', 'Customer', 'William', 'White', 27, 'M', 'william.white@example.com', '+1234567800', '456 Cedar St, Springfield, USA', NULL),
('elizabeth_martin', '123', 'Customer', 'Elizabeth', 'Martin', 36, 'F', 'elizabeth.martin@example.com', '+1234567801', '753 Maple St, Springfield, USA', NULL),
('joseph_thompson', '123', 'Customer', 'Joseph', 'Thompson', 40, 'M', 'joseph.thompson@example.com', '+1234567802', '159 Oak St, Springfield, USA', NULL),
('susan_clark', '123', 'Customer', 'Susan', 'Clark', 32, 'F', 'susan.clark@example.com', '+1234567803', '987 Birch St, Springfield, USA', NULL),
('thomas_lee', '123', 'Customer', 'Thomas', 'Lee', 38, 'M', 'thomas.lee@example.com', '+1234567804', '654 Pine St, Springfield, USA', NULL);


---- policy data

INSERT INTO Policies (PolicyID, PolicyType, PolicyName, InsuranceAmount, PolicyValidity, PolicyDescription, Available)
VALUES
(1, 'Life', 'Term Life Insurance', 500000.00, 20, 'Provides coverage for a fixed term of 20 years.', 'Y'),
(2, 'Life', 'Whole Life Insurance', 1000000.00, 100, 'Lifetime coverage with savings and protection.', 'Y'),
(3, 'Non-Life', 'Health Insurance', 300000.00, 5, 'Covers medical expenses for 5 years.', 'Y'),
(4, 'Non-Life', 'Auto Insurance', 150000.00, 3, 'Coverage for damage or theft of vehicles.', 'Y'),
(5, 'Non-Life', 'Home Insurance', 800000.00, 10, 'Provides coverage for home structure and contents.', 'Y'),
(6, 'Life', 'Endowment Policy', 600000.00, 15, 'Combines insurance with savings for 15 years.', 'Y'),
(7, 'Non-Life', 'Travel Insurance', 200000.00, 1, 'Covers travel-related risks including accidents.', 'Y'),
(8, 'Non-Life', 'Business Insurance', 5000000.00, 10, 'Provides coverage for business-related risks.', 'Y'),
(9, 'Life', 'Universal Life Insurance', 1200000.00, 30, 'Flexible life insurance with investment options.', 'Y'),
(10, 'Non-Life', 'Personal Accident Insurance', 250000.00, 5, 'Covers accidental death or injury.', 'Y'),
(11, 'Non-Life', 'Fire Insurance', 900000.00, 7, 'Covers damage to property due to fire.', 'Y'),
(12, 'Life', 'Child Insurance', 300000.00, 18, 'Provides coverage and savings for children.', 'Y'),
(13, 'Non-Life', 'Marine Insurance', 1500000.00, 5, 'Covers loss or damage of ships and cargo.', 'Y'),
(14, 'Life', 'Pension Plan', 800000.00, 25, 'Provides regular income post-retirement.', 'Y'),
(15, 'Non-Life', 'Pet Insurance', 100000.00, 2, 'Covers medical expenses for pets.', 'Y');



-- policy sold
INSERT INTO PolicySold (UserID, PolicyID, SoldDate, Amount, Duration)
VALUES
(140, 1, '2023-06-15', 500000.00, 20),
(141, 2, '2023-01-12', 1000000.00, 100),
(142, 3, '2023-07-22', 300000.00, 5),
(143, 4, '2023-03-30', 150000.00, 3),
(144, 5, '2023-08-01', 800000.00, 10),
(145, 6, '2023-02-17', 600000.00, 15),
(146, 7, '2023-04-10', 200000.00, 1),
(147, 8, '2023-09-05', 5000000.00, 10),
(148, 9, '2023-05-13', 1200000.00, 30),
(149, 10, '2023-03-28', 250000.00, 5),
(150, 11, '2023-06-25', 900000.00, 7),
(151, 12, '2023-07-20', 300000.00, 18),
(152, 13, '2023-02-15', 1500000.00, 5),
(153, 14, '2023-08-29', 800000.00, 25),
(154, 15, '2023-10-03', 100000.00, 2),
(142, 3, '2023-05-02', 300000.00, 5),
(143, 4, '2023-07-25', 150000.00, 3),
(144, 5, '2023-03-14', 800000.00, 10),
(145, 6, '2023-09-21', 600000.00, 15),
(146, 7, '2023-06-12', 200000.00, 1),
(147, 8, '2023-10-07', 5000000.00, 10),
(148, 9, '2023-08-17', 1200000.00, 30);





-- claims

INSERT INTO Claims (PurchaseId, ClaimAmount, RemainingAmount, ClaimDate)
VALUES
(25, 100000.00, 400000.00, '2024-01-15'),
(26, 200000.00, 800000.00, '2024-02-10'),
(27, 50000.00, 250000.00, '2024-03-05'),
(25, 15000.00, 385000.00, '2024-04-01'),
(28, 30000.00, 120000.00, '2024-05-12'),
(29, 80000.00, 420000.00, '2024-06-20'),
(30, 60000.00, 340000.00, '2024-07-15'),
(26, 50000.00, 750000.00, '2024-08-22'),
(27, 10000.00, 240000.00, '2024-09-30'),
(29, 20000.00, 400000.00, '2024-10-15');
