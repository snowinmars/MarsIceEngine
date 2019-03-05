powershell -Command 'magick.exe convert -transparent white +append .\cell\nop\*.png       .\cell-nop.png'

powershell -Command 'magick.exe convert -transparent white +append .\player\moveright\*.png .\player-moveright.png'
powershell -Command 'magick.exe convert -transparent white +append .\player\nop\*.png       .\player-nop.png'