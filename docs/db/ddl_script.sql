-- DROP SCHEMA public;

CREATE SCHEMA public AUTHORIZATION pg_database_owner;

COMMENT ON SCHEMA public IS 'standard public schema';
-- public.product_types определение

-- Drop table

-- DROP TABLE public.product_types;

CREATE TABLE public.product_types (
	type_name varchar NOT NULL,
	id serial4 NOT NULL,
	CONSTRAINT product_types_pk PRIMARY KEY (id),
	CONSTRAINT product_types_unique UNIQUE (type_name)
);


-- public.material_types определение

-- Drop table

-- DROP TABLE public.material_types;

CREATE TABLE public.material_types (
	id serial4 NOT NULL,
	"name" varchar NOT NULL,
	defects_percentage float4 NOT NULL,
	CONSTRAINT material_types_pk PRIMARY KEY (id)
);


-- public.partner_types определение

-- Drop table

-- DROP TABLE public.partner_types;

CREATE TABLE public.partner_types (
	id serial4 NOT NULL,
	"name" varchar NOT NULL,
	CONSTRAINT partner_types_pk PRIMARY KEY (id),
	CONSTRAINT partner_types_unique UNIQUE (name)
);


-- public.products определение

-- Drop table

-- DROP TABLE public.products;

CREATE TABLE public.products (
	id serial4 NOT NULL,
	type_id serial4 NOT NULL,
	"name" varchar NOT NULL,
	articul varchar NOT NULL,
	min_cost_for_partner numeric NOT NULL,
	CONSTRAINT products_articul_unique UNIQUE (articul),
	CONSTRAINT products_pk PRIMARY KEY (id),
	CONSTRAINT products_product_types_fk FOREIGN KEY (type_id) REFERENCES public.product_types(id) ON DELETE RESTRICT ON UPDATE CASCADE
);


-- public.partners определение

-- Drop table

-- DROP TABLE public.partners;

CREATE TABLE public.partners (
	id serial4 NOT NULL,
	partner_type_id serial4 NOT NULL,
	"name" varchar NOT NULL,
	director varchar NULL,
	email varchar NOT NULL,
	telephone_number varchar NOT NULL,
	inn varchar NOT NULL,
	rating int2 NOT NULL,
	legal_address varchar NULL,
	CONSTRAINT partners_pk PRIMARY KEY (id),
	CONSTRAINT partners_partner_types_fk FOREIGN KEY (partner_type_id) REFERENCES public.partner_types(id) ON DELETE RESTRICT ON UPDATE CASCADE
);


-- public.partners_products определение

-- Drop table

-- DROP TABLE public.partners_products;

CREATE TABLE public.partners_products (
	id serial4 NOT NULL,
	product_id serial4 NOT NULL,
	partner_id serial4 NOT NULL,
	amount serial4 NOT NULL,
	date_of_sale timestamp NOT NULL,
	CONSTRAINT partners_products_pk PRIMARY KEY (id),
	CONSTRAINT partners_products_partners_fk FOREIGN KEY (partner_id) REFERENCES public.partners(id) ON DELETE RESTRICT ON UPDATE CASCADE,
	CONSTRAINT partners_products_products_fk FOREIGN KEY (product_id) REFERENCES public.products(id) ON DELETE RESTRICT ON UPDATE CASCADE
);