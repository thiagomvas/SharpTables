# Change Log

All notable changes to this project will be documented in this file. See [versionize](https://github.com/versionize/versionize) for commit guidelines.

<a name="1.4.0"></a>
## [1.4.0](https://www.github.com/thiagomvas/SharpTables/releases/tag/v1.4.0) (2024-07-21)

### Features

* Add Line Graph and Scatter Graph support in GraphSettings ([e610bef](https://www.github.com/thiagomvas/SharpTables/commit/e610bef6e287f9a94496ca5fcf5bebb1f2d7d753))
* Add Table.AddRow(params string[]) and Table.SetHeader(params string[]) overloads ([86d3ceb](https://www.github.com/thiagomvas/SharpTables/commit/86d3ceb282a14511268db7877eaa60e35da43a61))

<a name="1.3.0"></a>
## [1.3.0](https://www.github.com/thiagomvas/SharpTables/releases/tag/v1.3.0) (2024-07-11)

### Features

* Add coloring to Graph ([f076a2e](https://www.github.com/thiagomvas/SharpTables/commit/f076a2e3968b0d8cf0f23ea43183361e799b8873))
* Add configuration methods for graph settings ([bc341e2](https://www.github.com/thiagomvas/SharpTables/commit/bc341e24d944e58c18b2db8f2d3c96b46d1304c5))
* Add Graph ([aa5245f](https://www.github.com/thiagomvas/SharpTables/commit/aa5245f5ff307314b78d3a667c6c64059d2780eb))
* Add Graph Header title ([cd6e4b8](https://www.github.com/thiagomvas/SharpTables/commit/cd6e4b8dc0f2b6e7076fc1b58b0901f509d1cfb5))
* Add graph pre-defined min and max values for y ticks ([49fa258](https://www.github.com/thiagomvas/SharpTables/commit/49fa2589609f5a0ad0a0ee32bdfce69c8cd9522d))
* Add GraphFormatting ([ed0abcf](https://www.github.com/thiagomvas/SharpTables/commit/ed0abcfb703d158b2820209340291b4bb382e6ed))
* Add GraphSettings<T> ([d780b07](https://www.github.com/thiagomvas/SharpTables/commit/d780b0736d8c1c609489d84d7d5faec29e49b0f8))
* Add IConsoleWriteable, implemented by Graph and Table ([de88e8b](https://www.github.com/thiagomvas/SharpTables/commit/de88e8be87061244f88772b3143364e66a617b25))
* Add pagination interface, implemented by paginated table and graph ([9fd914a](https://www.github.com/thiagomvas/SharpTables/commit/9fd914a9f5b7862d335d99f1c7a54a9b02153dfe))

<a name="1.2.1"></a>
## [1.2.1](https://www.github.com/thiagomvas/SharpTables/releases/tag/v1.2.1) (2024-07-05)

### Bug Fixes

* Cell.Position now is cloned properly ([4970ea8](https://www.github.com/thiagomvas/SharpTables/commit/4970ea8bd65c975afaa04baa3a8f991a457351c9))

<a name="1.2.0"></a>
## [1.2.0](https://www.github.com/thiagomvas/SharpTables/releases/tag/v1.2.0) (2024-07-04)

### Features

* Add PaginatedTable to allow table pagination ([840739c](https://www.github.com/thiagomvas/SharpTables/commit/840739c37e7e7fa3c1ecdda3769b8954ac84d130))
* Add Row Count and Row Indexes ([6221f92](https://www.github.com/thiagomvas/SharpTables/commit/6221f9244fc5916bfda55f14d76a6b1f970ebf4e))
* Add TableAlignmentAttribute ([c4dd839](https://www.github.com/thiagomvas/SharpTables/commit/c4dd839dad8f24f19d742249c8014ec0db9fca14))
* Add TableColorAttribute ([02e18fb](https://www.github.com/thiagomvas/SharpTables/commit/02e18fb8e6600107a94020f5924108e2a8cf4c94))
* Add TableDisplayNameAttribute to set a property display name on table. ([363da57](https://www.github.com/thiagomvas/SharpTables/commit/363da576943970c279a267f56aa79009e8c92cfc))
* Add TableIgnoreAttribute ([70d75af](https://www.github.com/thiagomvas/SharpTables/commit/70d75af0846c379fd17a588419c97bb35886dfee))
* Cells now have null value support. Cell.IsNull informs of a null value. ([576479e](https://www.github.com/thiagomvas/SharpTables/commit/576479ebaf100016ae5851c3e5816b6602cfe3ac))
* Methods that modify the table now return said tables reference ([77926e5](https://www.github.com/thiagomvas/SharpTables/commit/77926e5146cb0495e6d602cb7807cc5f87aec07c))
* Migrate Table settings to its own class ([b503094](https://www.github.com/thiagomvas/SharpTables/commit/b50309488321251b76e4ff78d9535b06bc5119c8))
* New Add/From DataSet methods using reflection to add all props to a table ([e4c2b6d](https://www.github.com/thiagomvas/SharpTables/commit/e4c2b6d87590329dd7b10592a00a33400c33155c))

### Bug Fixes

* ToMarkdown and ToHtml wouldn't replace null or empty values ([87a1901](https://www.github.com/thiagomvas/SharpTables/commit/87a190191319de3bac0f75b2c3ed90210c507193))

<a name="1.1.0"></a>
## [1.1.0](https://www.github.com/thiagomvas/SharpTables/releases/tag/v1.1.0) (2024-06-29)

### Features

* Add Cell.Type, Cell.IsBool, Cell.IsNumeric ([7acd602](https://www.github.com/thiagomvas/SharpTables/commit/7acd602d77367d2976b93cf6326f6c85a834ad85))
* Add Table.AddDataSet() with same overloads as Table.FromDataSet() ([a6e21b4](https://www.github.com/thiagomvas/SharpTables/commit/a6e21b41779652466da013497884187c3e07f6f2))
* Add Table.ToHtml() ([aacda1a](https://www.github.com/thiagomvas/SharpTables/commit/aacda1ad0839b800c9cb0b97d480047a1fa836b3))
* Add Table.ToMarkdown() ([7397259](https://www.github.com/thiagomvas/SharpTables/commit/7397259bf556c5d2895a4eca80d6e81479a014ed))
* Added presetters for each cell and removed old presetting methods ([d17a9c8](https://www.github.com/thiagomvas/SharpTables/commit/d17a9c81c3ab7548e9071c3bfbdfe2eb6332a357))
* New header implementation. Use SetHeader to define a header. ([25ddf7b](https://www.github.com/thiagomvas/SharpTables/commit/25ddf7b6babbef4ff9f3db96a496bce58d8a110c))
* Remove Number and Text alignment in favour of per cell alignment ([ce5c797](https://www.github.com/thiagomvas/SharpTables/commit/ce5c797fa4095e0897793ced50b73dd666faab13))

### Bug Fixes

* Cell presets now apply to all cells, including ones added after preset is defined ([6a90eb5](https://www.github.com/thiagomvas/SharpTables/commit/6a90eb5c65b3b435464c317e43866e212f895579))

<a name="1.1.0"></a>
## [1.0.0](https://www.github.com/thiagomvas/SharpTables/releases/tag/v1.0.0) (2024-06-28)

### Features

* Added presetters for each cell and removed old presetting methods ([d17a9c8](https://www.github.com/thiagomvas/SharpTables/commit/d17a9c81c3ab7548e9071c3bfbdfe2eb6332a357))
* Added new FromDataSet overloads using factory pattern ([d17a9c8](https://www.github.com/thiagomvas/SharpTables/commit/d17a9c81c3ab7548e9071c3bfbdfe2eb6332a357))
* Added Row.LineIndex and Cell.Position ([d17a9c8](https://www.github.com/thiagomvas/SharpTables/commit/d17a9c81c3ab7548e9071c3bfbdfe2eb6332a357))
* New header implementation. Use SetHeader to define a header. ([25ddf7b](https://www.github.com/thiagomvas/SharpTables/commit/25ddf7b6babbef4ff9f3db96a496bce58d8a110c))
* Remove Number and Text alignment in favour of per cell alignment ([ce5c797](https://www.github.com/thiagomvas/SharpTables/commit/ce5c797fa4095e0897793ced50b73dd666faab13))

