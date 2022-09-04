DROP TYPE IF EXISTS type_of_product;
CREATE TYPE type_of_product AS ENUM
('Lotion', 'Serum', 'Cleaning', 'Mask');

DROP TYPE IF EXISTS function_of_product;
CREATE TYPE function_of_product AS ENUM
('Rejuvenating', 'Restoring');

DROP TYPE IF EXISTS type_of_skin;
CREATE TYPE type_of_skin AS ENUM
('Soft', 'Sensitive', 'Rough');

DROP TABLE IF EXISTS products; 
CREATE TABLE products
(
    product_id serial PRIMARY KEY,
    product_name varchar(32) UNIQUE NOT NULL,
    product_price int NOT NULL,
    product_type type_of_product[] NOT NULL,
    product_function function_of_product[] NOT NULL,
    skin_type type_of_skin[] NOT NULL,
    image_name varchar(32) UNIQUE NOT NULL
);

INSERT INTO products(product_name, product_price, product_type, product_function, skin_type, image_name)
VALUES
('Крем с ретинолом', 1000, '{"Lotion"}', '{"Rejuvenating", "Restoring"}', '{"Soft"}', '1_1'),
('Лосьон с центеллой', 1500, '{"Serum"}', '{"Rejuvenating"}', '{"Soft", "Sensitive"}', '1_2'),
('Серум с зеленым чаем', 1200, '{"Serum"}', '{"Restoring"}', '{"Soft"}', '1_3'),
('Сыворотка с полынью', 1600, '{"Lotion"}', '{"Restoring"}', '{"Soft"}', '1_4'),
('Скраб с ванилью', 900, '{"Cleaning"}', '{"Rejuvenating", "Restoring"}', '{"Soft", "Sensitive"}', '2_1'),
('Крем под глаза', 2000, '{"Lotion"}', '{"Rejuvenating", "Restoring"}', '{"Sensitive"}', '2_2'),
('Крем с мятой', 1000, '{"Lotion"}', '{"Rejuvenating"}', '{"Soft", "Sensitive"}', '2_3'),
('Глиняная маска с матча', 800, '{"Mask"}', '{"Restoring"}', '{"Sensitive"}', '2_4'),
('Черенок от лопаты', 500, '{"Cleaning"}', '{"Restoring"}', '{"Rough"}', '3_1'),
('Гараж', 10000, '{"Cleaning"}', '{"Rejuvenating", "Restoring"}', '{"Rough"}', '3_2'),
('Сосиска в тесте', 50, '{"Serum"}', '{"Rejuvenating", "Restoring"}', '{"Rough"}', '3_3');

SELECT *
FROM products;


DROP TABLE IF EXISTS customers; 
CREATE TABLE customers
(
    customer_id serial PRIMARY KEY,
    first_name varchar(32) NOT NULL,
    last_name varchar(32) NOT NULL,
    phone_number varchar(10) UNIQUE NOT NULL,
    customer_password varchar(16) NOT NULL,
    customer_email varchar(32) UNIQUE NOT NULL
);

DROP TABLE IF EXISTS customer_favourites; 
CREATE TABLE customer_favourites
(
    customer_id int,
    product_id int,
	add_date date,
	
    PRIMARY KEY (customer_id, product_id),
	
    CONSTRAINT FK_favourites_customer_id FOREIGN KEY (customer_id) REFERENCES customers(customer_id),
    CONSTRAINT FK_favourites_product_id FOREIGN KEY (product_id) REFERENCES products(product_id)
);

DROP TABLE IF EXISTS customer_cart; 
CREATE TABLE customer_cart
(
    customer_id int,
    product_id int,
	quantity int,
    PRIMARY KEY (customer_id, product_id),
	
    CONSTRAINT FK_cart_customer_id FOREIGN KEY (customer_id) REFERENCES customers(customer_id),
    CONSTRAINT FK_cart_product_id FOREIGN KEY (product_id) REFERENCES products(product_id)
);

DROP TABLE IF EXISTS orders; 
CREATE TABLE orders
(
    order_id serial PRIMARY KEY,
    customer_id int,
    
    CONSTRAINT FK_favourites_customer_id FOREIGN KEY (customer_id) REFERENCES customers(customer_id)
);

DROP TABLE IF EXISTS order_details; 
CREATE TABLE order_details
(
    order_id serial,
    product_id int,
    unit_price real NOT NULL,
    quantity int NOT NULL,
    discount real NOT NULL,
	PRIMARY KEY (order_id, product_id),
    
	CONSTRAINT FK_details_order_id FOREIGN KEY (order_id) REFERENCES orders(order_id),
    CONSTRAINT FK_details_product_id FOREIGN KEY (product_id) REFERENCES products(product_id)
);





