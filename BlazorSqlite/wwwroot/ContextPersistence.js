let fileSystemHandler = await navigator.storage.getDirectory();
let databaseFileHandler = await fileSystemHandler.getFileHandle("blazorschool.db", { create: true });

export async function PersistDataAsync(fileByteContent)
{
    let writeStream = await databaseFileHandler.createWritable();
    await writeStream.write(fileByteContent.buffer);
    writeStream.close();
}

export async function LoadDataAsync()
{
    let databaseFile = await databaseFileHandler.getFile();
    let arrayBuffer = await databaseFile.arrayBuffer();
    var byteArray = new Uint8Array(arrayBuffer);

    return byteArray;
}