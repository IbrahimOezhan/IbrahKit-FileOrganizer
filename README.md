# IbrahKit-FileOrganizer

A windows CLI tool for organizing files in a folder into sub folders.

- Adds random numbers to the end of a file if one with the same name is already found
- Supports custom configurations through the file that gets generated on first launch.

The default configuration sorts files the following:

| Folder       | File Extensions                                                                 |
|--------------|----------------------------------------------------------------------------------|
| **Images**       | png, jpg, jpeg, gif, bmp, tiff, webp, svg, ico, heic, raw                       |
| **Videos**       | mp4, mov, avi, mkv, webm, flv, wmv, m4v                                          |
| **Audio**        | mp3, wav, ogg, flac, aac, wma, m4a                                              |
| **Documents**    | pdf, doc, docx, xls, xlsx, ppt, pptx, txt, rtf, odt, ods, odp, md, csv          |
| **Archives**     | zip, rar, 7z, tar, gz, bz2, xz, iso                                             |
| **Executables**  | exe, msi, bat, cmd, sh, app, apk                                                |
| **Code**         | cs, js, ts, html, css, cpp, h, c, java, py, rb, php, go, json, xml, yaml, yml, sql |
| **Directories**  | Sub-folders that don’t match a file type                                       |
| **Other**        | Everything else                                                                |
