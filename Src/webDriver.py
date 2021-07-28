from selenium import webdriver

driver = webdriver.Chrome(r"C:\Users\Nicho\Downloads\chromedriver_win32\chromedriver.exe")
driver.get("https://www.chess.com/")

# This will get the initial newHtml - before javascript
html1 = driver.page_source

# This will get the newHtml after on-load javascript
html2 = driver.execute_script("return document.documentElement.innerHTML;")

oldHtml=""
while(True):
    # This will get the newHtml after on-load javascript
    newHtml = driver.execute_script("return document.documentElement.innerHTML;")

    if(oldHtml !=newHtml):
        print(newHtml)
    oldHtml=newHtml

