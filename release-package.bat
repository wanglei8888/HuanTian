@echo off
chcp 65001 >nul
title 发布打包 - 仅拷贝前端 dist + 后端
color 0A
setlocal EnableDelayedExpansion

:: =====================================================
:: 路径配置
:: =====================================================

:: 服务打包最终地址（😎）
set "releaseDir=D:\UserData\Desktop\release" 
:: 后端代码生成后地址
set "serviceDir=%releaseDir%\Service"
:: 前端代码生成后的地址
set "webDir=%releaseDir%\Web"
:: 前端代码地址(代码地址，用于build)
set "webProjectDir=E:\ProjectCode\ITF\ITF.ERP.Web"
:: 后端代码地址(代码地址，用于build)
set "apiProjectDir=E:\ProjectCode\ITF\ITF.ERP.Server\ITF.Admin.WebApi"
:: 压缩包最终地址
set "zipFile=%releaseDir%\release.zip"
:: 压缩包，压缩临时地址
set "zipPs1=%TEMP%\do_package_release.ps1"

:: 日志开关（1=显示日志；0=静默+动画）
set "PRINT_FE_LOG=0"
set "PRINT_BE_LOG=0"
set "PRINT_ZIP_LOG=0"

set "TOTAL_CS=0"
set "DO_FE=0"
set "DO_BE=0"
set "CHOICE_DESC=前后端全部"
goto :MAIN

:: =====================================================
:: 工具函数
:: =====================================================

:TS_NOW
setlocal EnableDelayedExpansion
set "T=%time: =0%"
for /f "tokens=1-4 delims=:.," %%a in ("%T%") do (
  set /a __hh=1%%a-100, __mm=1%%b-100, __ss=1%%c-100, __cc=1%%d-100
)
set /a __total=(((__hh*60)+__mm)*60+__ss)*100+__cc
for /f "delims=" %%# in ("!__total!") do endlocal & set "%~1=%%#"
goto :eof

:CS_DIFF
setlocal EnableDelayedExpansion
set /a _d=!%~2!-!%~1!
if !_d! lss 0 set /a _d+=8640000
for /f "delims=" %%# in ("!_d!") do endlocal & set "%~3=%%#"
goto :eof

:CS_FORMAT
setlocal EnableDelayedExpansion
set /a _t=!%~1!, _hh=_t/360000, _rem=_t%%360000, _mm=_rem/6000, _rem=_rem%%6000, _ss=_rem/100, _cs=_rem%%100
set "_HH=0!_hh!" & set "_MM=0!_mm!" & set "_SS=0!_ss!" & set "_CC=0!_cs!"
set "_HH=!_HH:~-2!" & set "_MM=!_MM:~-2!" & set "_SS=!_SS:~-2!" & set "_CC=!_CC:~-2!"
for /f "delims=" %%# in ("!_HH!:!_MM!:!_SS!.!_CC!") do endlocal & set "%~2=%%#"
goto :eof

:RUN_SPINNER
powershell -NoLogo -NoProfile -ExecutionPolicy Bypass -Command ^
  "[Console]::OutputEncoding=[Text.Encoding]::UTF8;" ^
  "$raw='🌱,🧟,☘️,🌸,🌺,🌻,🌿,🛌,🗽,💍,💵,😴,🌷,🌼,';" ^
  "$frames=$raw -split ',' | Where-Object { $_ -ne '' };" ^
  "$i=0; $cmd=$env:SPIN_CMD; $prompt=$env:SPIN_PROMPT;" ^
  "$p = Start-Process -FilePath 'cmd.exe' -ArgumentList '/c', $cmd -WindowStyle Hidden -PassThru;" ^
  "while(-not $p.HasExited){" ^
  " Write-Host (' '+$prompt+' '+$frames[$i %% $frames.Count]) -NoNewline;" ^
  " Start-Sleep -Milliseconds 200;" ^
  " Write-Host -NoNewline \"`r\"; $i++ };" ^
  "Write-Host (' '+$prompt+' ✨ 完成');" ^
  "try { $exit=$p.ExitCode } catch { $exit=1 };" ^
  "exit $exit"
exit /b %errorlevel%


:PAUSE_NICE
echo ⏹️ 按任意键退出...
pause >nul
exit /b

:ENSURE_DIR
:: 用法：call :ENSURE_DIR "完整路径"  —— 成功返回 0，失败返回 1
setlocal EnableDelayedExpansion
set "TARGET=%~1"

:: 已是目录
if exist "!TARGET!\" (
  endlocal & exit /b 0
)

:: 存在同名文件 -> 备份后创建目录
if exist "!TARGET!" (
  echo ⚠️ [警告] 发现同名文件占用：!TARGET!
  set "TS=%date:~0,4%%date:~5,2%%date:~8,2%_%time: =0%"
  set "TS=!TS::=!"
  set "TS=!TS:.=!"
  set "BAK=!TARGET!.file_!TS!.bak"
  attrib -R -S -H "!TARGET!" >nul 2>&1
  move /y "!TARGET!" "!BAK!" >nul || (
    echo ❌ [错误] 无法移动同名文件，请手动处理：!TARGET!
    endlocal & exit /b 1
  )
  attrib -R -S -H "!BAK!" >nul 2>&1
  echo ℹ️ [信息] 已将同名文件备份为：!BAK!
)

mkdir "!TARGET!" 2>nul || (
  echo ❌ [错误] 无法创建目录：!TARGET!
  endlocal & exit /b 1
)

endlocal & exit /b 0

:DELETE_BAK
:: 用法：call :DELETE_BAK "pattern"   例如：call :DELETE_BAK "%releaseDir%\Web.file_*.bak"
setlocal EnableDelayedExpansion
set "PATTERN=%~1"
set "DEL_CNT=0"

for /f "delims=" %%F in ('dir /b /a:-d "%PATTERN%" 2^>nul') do (
  echo 🧽 [清理] 尝试删除备份文件：%%~fF
  attrib -R -S -H "%%~fF" >nul 2>&1
  del /f /q "%%~fF" >nul 2>&1
  if exist "%%~fF" (
    rem 尝试使用 PowerShell 强制删除（处理权限/占用问题）
    powershell -NoLogo -NoProfile -Command "try{ Remove-Item -LiteralPath '%%~fF' -Force -ErrorAction Stop } catch {}" >nul 2>&1
  )
  if not exist "%%~fF" (
    echo ✅ [完成] 已删除：%%~nxF
    set /a DEL_CNT+=1
  ) else (
    echo ⚠️ [警告] 删除失败（可能被占用或权限不足）：%%~nxF
  )
)

endlocal & exit /b 0

:: =====================================================
:: 主流程
:: =====================================================
:MAIN
echo =====================================================
echo ℹ️ [信息] 开始打包
echo =====================================================
echo.

echo 请选择本次打包范围：
echo   1 = 仅打包后端
echo   2 = 仅打包前端
echo   3 = 同时打包前后端
choice /c 123 /n /m "请输入选择 (1/2/3)："
set "PKG_OPTION=%errorlevel%"
if "%PKG_OPTION%"=="1" (
  set "DO_BE=1"
  set "CHOICE_DESC=仅后端"
) else (
  if "%PKG_OPTION%"=="2" (
    set "DO_FE=1"
    set "CHOICE_DESC=仅前端"
  ) else (
    set "DO_FE=1"
    set "DO_BE=1"
    set "CHOICE_DESC=前后端全部"
  )
)

echo 🗳️ [选择] 本次将打包：!CHOICE_DESC!
echo.

set startTime=%time%

if "%DO_FE%"=="1" (
  :: ---------- [1/4] 前端构建 ----------
  call :TS_NOW FE_T0
  echo 🧱 [1/4] 正在构建前端...
  pushd "%webProjectDir%" || (
    echo ❌ [错误] 无法进入目录："%webProjectDir%"
    call :PAUSE_NICE
    goto :end
  )

  echo ℹ️ [信息] 执行命令：yarn build:prod
  if "%PRINT_FE_LOG%"=="1" (
    call yarn build:prod
  ) else (
    set "SPIN_CMD=yarn build:prod"
    set "SPIN_PROMPT=前端构建中，请稍候"
    call :RUN_SPINNER
  )
  if errorlevel 1 (
    echo ⚠️ [警告] 前端构建返回错误，将继续检查 dist 目录
  ) else (
    echo ✅ [完成] 前端构建完成
  )
  popd

  call :TS_NOW FE_T1
  call :CS_DIFF FE_T0 FE_T1 FE_ELAPSED_CS
  set /a TOTAL_CS+=FE_ELAPSED_CS
  call :CS_FORMAT FE_ELAPSED_CS FE_ELAPSED
  echo ⏱️ [耗时] 前端构建：!FE_ELAPSED!
  echo.

  :: ---------- [1.1] 复制前端 dist ----------
  call :TS_NOW CP_T0
  powershell -NoLogo -NoProfile -Command "Write-Host '📦 [1.1] 正在复制前端 dist -> %webDir%'"


  if not exist "%webProjectDir%\dist\" (
    echo ❌ [错误] 未找到前端输出目录："%webProjectDir%\dist\"
    echo 👉 请检查前端构建或修改输出路径。
    call :PAUSE_NICE
    goto :end
  )

  :: 确保 release 根目录存在
  if not exist "%releaseDir%\" mkdir "%releaseDir%"

  :: 确保 Web 为“目录”（如遇同名文件会自动备份）
  call :ENSURE_DIR "%webDir%"
  if errorlevel 1 (
    call :PAUSE_NICE
    goto :end
  )

  :: 使用 robocopy 复制（不使用 /MIR，避免清空目标）
  robocopy "%webProjectDir%\dist" "%webDir%" /E /NFL /NDL /NJH /NJS /NP > "%TEMP%\robocopy_dist.log"

  :: ---------- [1.1 结束] 复制前端计时 ----------
  call :TS_NOW CP_T1
  call :CS_DIFF CP_T0 CP_T1 CP_ELAPSED_CS
  set /a TOTAL_CS+=CP_ELAPSED_CS
  call :CS_FORMAT CP_ELAPSED_CS CP_ELAPSED
  echo ⏱️ [耗时] 复制前端：!CP_ELAPSED!
  echo.
) else (
  echo 🧱 [1/4] 未选择前端，跳过前端构建与复制。
  echo.
)

if "%DO_BE%"=="1" (
  :: ---------- [2/4] 后端构建 ----------
  call :TS_NOW BE_T0
  echo 🔧 [2/4] 正在构建后端...
  pushd "%apiProjectDir%" || (
    echo ❌ [错误] 无法进入目录："%apiProjectDir%"
    call :PAUSE_NICE
    goto :end
  )

  :: 确保 Service 为“目录”（如遇同名文件会自动备份）
  call :ENSURE_DIR "%serviceDir%"
  if errorlevel 1 (
    popd
    call :PAUSE_NICE
    goto :end
  )

  if "%PRINT_BE_LOG%"=="1" (
    dotnet publish -c Release -o "%serviceDir%"
  ) else (
    set "SPIN_CMD=dotnet publish -c Release -o ^"!serviceDir!^""
    set "SPIN_PROMPT=后端构建中，请稍候"
    call :RUN_SPINNER
  )
  if errorlevel 1 (
    echo ⚠️ [警告] 后端构建返回错误
  ) else (
    echo ✅ [完成] 后端构建完成
  )
  popd

  call :TS_NOW BE_T1
  call :CS_DIFF BE_T0 BE_T1 BE_ELAPSED_CS
  set /a TOTAL_CS+=BE_ELAPSED_CS
  call :CS_FORMAT BE_ELAPSED_CS BE_ELAPSED
  echo ⏱️ [耗时] 后端构建：!BE_ELAPSED!
  echo.

  :: ---------- [3/4] 清理配置 ----------
  call :TS_NOW RM_T0
  echo 🧹 [3/4] 检查并删除 Service 中的 appsettings.json（如存在）...
  if exist "%serviceDir%\appsettings.json" (
    del /f /q "%serviceDir%\appsettings.json"
    echo ✅ [完成] 已删除 appsettings.json
  ) else (
    echo ✅ [完成] 未找到，跳过
  )
  call :TS_NOW RM_T1
  call :CS_DIFF RM_T0 RM_T1 RM_ELAPSED_CS
  set /a TOTAL_CS+=RM_ELAPSED_CS
  call :CS_FORMAT RM_ELAPSED_CS RM_ELAPSED
  echo ⏱️ [耗时] 配置清理：!RM_ELAPSED!
  echo.
) else (
  echo 🔧 [2/4] 未选择后端，跳过后端构建与配置清理。
  echo.
)

if "%DO_FE%"=="0" if "%DO_BE%"=="0" (
  echo ❌ [错误] 未选择任何打包内容，程序结束。
  goto :end
)

:: ---------- [4/4] 打包 ----------
call :TS_NOW ZP_T0
echo 📦 [4/4] 正在打包 -> %zipFile% ...
if not exist "%releaseDir%" mkdir "%releaseDir%"
if exist "%zipFile%" del /f /q "%zipFile%"

:: 逐行写入 PowerShell 打包脚本（避免 cmd 分组导致的 { 解析问题）
> "%zipPs1%" echo $ErrorActionPreference='Stop'
>> "%zipPs1%" echo $zip = '%zipFile%'; $release = '%releaseDir%';
>> "%zipPs1%" echo if ^(Test-Path $zip^) { Remove-Item -Force $zip }
>> "%zipPs1%" echo $items = @^()
>> "%zipPs1%" echo if ^($env:DO_FE -eq '1'^) { $items += ^(Join-Path $release 'Web'^) }
>> "%zipPs1%" echo if ^($env:DO_BE -eq '1'^) { $items += ^(Join-Path $release 'Service'^) }
>> "%zipPs1%" echo if ^(-not $items^) { throw '未选择任何打包内容' }
>> "%zipPs1%" echo Compress-Archive -Path $items -DestinationPath $zip -Force

if "%PRINT_ZIP_LOG%"=="1" (
  powershell -NoLogo -NoProfile -File "%zipPs1%"
) else (
  set "SPIN_CMD=powershell -NoLogo -NoProfile -File ^"%zipPs1%^""
  set "SPIN_PROMPT=压缩打包中，请稍候"
  call :RUN_SPINNER
)

if errorlevel 1 (
  echo ❌ [错误] 打包失败
  call :PAUSE_NICE
  goto :end
) else (
  echo ✅ [完成] 已生成压缩包：%zipFile%
)

:: ---------- 打包后清理：删除 .bak 备份 ----------
echo 🧽 [清理] 正在删除打包过程中产生的 .bak 备份文件...
call :DELETE_BAK "%releaseDir%\Web.file_*.bak"
call :DELETE_BAK "%releaseDir%\Service.file_*.bak"

call :TS_NOW ZP_T1
call :CS_DIFF ZP_T0 ZP_T1 ZP_ELAPSED_CS
set /a TOTAL_CS+=ZP_ELAPSED_CS
call :CS_FORMAT ZP_ELAPSED_CS ZP_ELAPSED
echo ⏱️ [耗时] 压缩打包（含清理）：!ZP_ELAPSED!
echo.

:: ---------- 清理提示 ----------
call :TS_NOW CL_T0
if "%DO_FE%"=="1" (
  echo 🗂️ [保留] 已保留发布目录下的 Web（不做删除操作）
) else (
  echo 🗂️ [保留] 未选择前端，本次无需处理 Web 目录
)
call :TS_NOW CL_T1
call :CS_DIFF CL_T0 CL_T1 CL_ELAPSED_CS
set /a TOTAL_CS+=CL_ELAPSED_CS
call :CS_FORMAT CL_ELAPSED_CS CL_ELAPSED
echo ⏱️ [耗时] 清理提示：!CL_ELAPSED!
echo.

:end
echo =====================================================
echo 🏁 [完成] 打包流程结束
echo 📦 压缩包：%zipFile%
set endTime=%time%
call :CS_FORMAT TOTAL_CS TOTAL_ELAPSED
echo.

echo -----------------------------------------------------
echo ⏱️ 开始时间：%startTime%
echo ⏱️ 结束时间：%endTime%
echo =====================================================
echo ✅ 按任意键打开发布目录并退出...
pause >nul
explorer "%releaseDir%"
endlocal
exit
