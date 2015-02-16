SELECT * FROM CATEGORY
SELECT * FROM PRODUCT

--Strictly only matching rows
SELECT * FROM Category C
INNER JOIN Product P
	ON C.Id = P.CategoryId

--Return all rows from LEFT table, NULL whereever no matching rows in RIGHT
SELECT * FROM Category C
LEFT JOIN Product P
	ON C.Id = P.CategoryId

--Return all rows from RIGHT table, NULL whereever no matching rows in LEFT
SELECT * FROM Category C
RIGHT JOIN Product P
	ON C.Id = P.CategoryId

--Return all rows from both tables, NULL whereever no matching rows
SELECT * FROM Category C
FULL OUTER JOIN Product P
	ON C.Id = P.CategoryId

--Consider all unique values
SELECT Name FROM Category
UNION
SELECT TITLE FROM Product

--Consider all value including duplicates
SELECT Name FROM Category
UNION ALL
SELECT TITLE FROM Product

--Group by
SELECT C.Id, C.Name, Count(C.Id) AS OrderCount FROM Product P
INNER JOIN Category C
ON P.CategoryId=C.Id
GROUP BY C.Id, C.Name

--Group by and Having
SELECT C.Id, C.Name, COUNT(C.Id) AS OrderCount FROM Product P
INNER JOIN Category C
ON P.CategoryId=C.Id
GROUP BY C.Id, C.Name
HAVING COUNT(C.Id) > 1


SELECT C.Id, C.Name, COUNT(C.Id) AS OrderCount FROM Product P
INNER JOIN Category C
ON P.CategoryId=C.Id
GROUP BY C.Id, C.Name
HAVING COUNT(C.Id) BETWEEN 0 AND 1

--Below both queries are returned same result
SELECT C.Id, C.Name, COUNT(C.Id) AS OrderCount FROM Product P
INNER JOIN Category C
ON P.CategoryId=C.Id
GROUP BY C.Id, C.Name
HAVING C.Name LIKE '%Laptop%'

SELECT C.Id, C.Name, COUNT(C.Id) AS OrderCount FROM Product P
INNER JOIN Category C
ON P.CategoryId=C.Id
WHERE C.Name LIKE '%Laptop%'
GROUP BY C.Id, C.Name

SELECT C.Id, C.Name, COUNT(C.Id) AS OrderCount FROM Product P
INNER JOIN Category C
ON P.CategoryId=C.Id
GROUP BY C.Id, C.Name
HAVING SUM(P.CurrentPrice) > 1000

SELECT * FROM Product P
SELECT * FROM Category C


