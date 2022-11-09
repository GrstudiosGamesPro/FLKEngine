--Script: Script_6945
--Do not delete the OnUpdate and OnStart functions because without them the script will not work

---------------------------



--Your variables in here



---------------------------


function OnStart()
--write your code here 
--The code written here will be initialized when starting the game
end



function OnUpdate()

    if Input:OnKeyDown("T") then
        --Este OnKeyDown("TECLA") ejecutara codigo dentro del if al presionar la tecla
    end

    if Input:OnKeyPressed("T") then
        --Este OnKeyDown("TECLA") ejecutara codigo dentro del if al mantener la tecla presionada
    end

    if Input:OnKeyUp("T") then
        --Este OnKeyDown("TECLA") ejecutara codigo dentro del if al soltar la tecla
    end

end