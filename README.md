[![Build Status](https://travis-ci.org/afonsof/OFXSharp.svg)](https://travis-ci.org/afonsof/OFXSharp)
[![Built with Grunt](https://cdn.gruntjs.com/builtwith.png)](http://gruntjs.com/)

# OFXSharp

.NET OFX Parser

## Getting Started

#### How to use
```c#
var parser = new OFXDocumentParser();
var ofxDocument = parser.Import(new FileStream(@"c:\ofxdoc.ofx", FileMode.Open));
```

## Contributing

Please use the issue tracker and pull requests.

## License
Copyright (c) 2014 Afonso França, Antônio Milesi Bastos, James Hollingworth
Licensed under the MIT license.
