const fs = require('fs');

const read = fs.readFileSync(process.argv[2], 'utf8');
fs.writeFileSync(process.argv[2],
  read.includes('PackageId')
    ? read.replace(/<PackageId>.*?</PackageId>/g, `<PackageId>${process.argv[3]}</PackageId>`)
    : read.replace(/<PropertyGroup>/, `<PropertyGroup><PackageId>${process.argv[3]}</PackageId>`)
);