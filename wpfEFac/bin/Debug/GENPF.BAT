set /P cert= Introduzca el nombre del certificado (sin extension) :
set /P pass= Introduzca la password del certificado :
set /P pass2= Introduzca la password del pfx :

OPENSSL.EXE pkcs8 -inform DER -in %cert%.key -passin pass:%pass% -out %cert%.key.pem

OPENSSL.EXE x509 -in %cert%.cer -inform d -out %cert%.cer.pem


echo "'//3. Unificar los dos ficheros en formato PEM en uno con formato PFX"
OPENSSL.EXE pkcs12 -export -in %cert%.cer.pem -inkey %cert%.key.pem -passin pass:%pass% -passout pass:%pass2% -out %cert%.pfx



pause