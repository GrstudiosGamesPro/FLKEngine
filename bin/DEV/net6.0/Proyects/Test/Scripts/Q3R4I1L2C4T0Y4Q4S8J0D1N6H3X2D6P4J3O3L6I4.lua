--Script: Script_7359
--Do not delete the OnUpdate and OnStart functions because without them the script will not work

---------------------------



--Your variables in here



---------------------------



function OnStart()
    Data:SaveString("Prueba", "DatoGuardado")
end



function OnUpdate()
    Console:Log("Valor Obtenido: " .. Data:GetSaveString("Prueba"))
end