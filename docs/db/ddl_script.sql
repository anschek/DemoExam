-- public.material_types определение

-- Drop table

-- DROP TABLE public.material_types;

CREATE TABLE public.material_types (
	id serial4 NOT NULL,
	"name" varchar NOT NULL,
	CONSTRAINT material_types_pk PRIMARY KEY (id)
);


-- public.suppliers определение

-- Drop table

-- DROP TABLE public.suppliers;

CREATE TABLE public.suppliers (
	id serial4 NOT NULL,
	"name" varchar NOT NULL,
	CONSTRAINT suppliers_pk PRIMARY KEY (id)
);


-- public.materials определение

-- Drop table

-- DROP TABLE public.materials;

CREATE TABLE public.materials (
	id serial4 NOT NULL,
	"type" serial4 NOT NULL,
	"name" varchar NOT NULL,
	amount int4 NOT NULL,
	"cost" numeric NOT NULL,
	description varchar NULL,
	minimal_amount int4 NOT NULL,
	unit varchar NOT NULL,
	pack_quantity int4 NOT NULL,
	image varchar NULL,
	CONSTRAINT materials_pk PRIMARY KEY (id),
	CONSTRAINT materials_material_types_fk FOREIGN KEY ("type") REFERENCES public.material_types(id) ON DELETE RESTRICT ON UPDATE CASCADE
);


-- public.materials_suppliers определение

-- Drop table

-- DROP TABLE public.materials_suppliers;

CREATE TABLE public.materials_suppliers (
	supplier serial4 NOT NULL,
	material serial4 NOT NULL,
	CONSTRAINT materials_suppliers_pk PRIMARY KEY (supplier, material),
	CONSTRAINT materials_suppliers_materials_fk FOREIGN KEY (material) REFERENCES public.materials(id) ON DELETE RESTRICT ON UPDATE CASCADE,
	CONSTRAINT materials_suppliers_suppliers_fk FOREIGN KEY (supplier) REFERENCES public.suppliers(id) ON DELETE RESTRICT ON UPDATE CASCADE
);