const fs = require('fs');

fs.writeFileSync(process.argv[2], fs.readFileSync(process.argv[2], 'utf8').replace(/<PropertyGroup>/, '<PropertyGroup>\r\n    <PackageId>' + process.argv[3] + '</PackageId>'));