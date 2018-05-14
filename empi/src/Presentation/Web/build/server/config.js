const fs = require('fs');
var glob = require("glob");

const args = process.argv;
if (args.length < 3) {
  console.log('Set the api server location, Usage:');
  console.log('   node config <api address>');
  return;
}
const apiHost = args[2];

glob("./dist/*.js", (er, files) => {
  if (er) {
    console.log(er);
    return;
  }
  if (!files || files.length < 1) {
    console.log('No js files were found!');
    return;
  }


  for (let filePath of files) {
    let found = false;
    var contents = fs.readFileSync(filePath, 'utf8').toString();
    if (!contents) return;

    let result = contents.replace(
      /apiServer\s*:\s*([',"]).+?\1/,
      (ori, firstMatch) => {
        found = true;
        return `apiServer:${firstMatch}${apiHost}${firstMatch}`;
      }
    );

    if (found) {
      fs.writeFileSync(filePath, result, 'utf8');
      console.log(`${filePath} was updated!`);
    }
  }
});