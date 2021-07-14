#include "AjaxHelper.h"

bool AjaxHelper::IsNumeric(const std::string str)
{
    std::string::const_iterator iterator=str.begin();
    while (iterator != str.end() && std::isdigit(*iterator))
        iterator++;

    return iterator==str.end()&&str.size()>0;
}
