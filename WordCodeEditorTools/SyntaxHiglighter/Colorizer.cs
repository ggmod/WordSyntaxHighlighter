using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace WordCodeEditorTools.SyntaxHighlighter
{
    /// <summary>
    /// Represents a colorizer that provides a way to syntax highlight text using 
    /// the programming language definitions and their assigned styles that the colorizer contains.
    /// </summary>
    [DataContract(Name = "Colorizer", Namespace = "SyntaxHighlighter_2.0")]
    public class Colorizer
    {
        /// <summary>
        /// Stores the language definitions (their syntactic rules and the styles assigned to the language elements)
        /// </summary>
        [DataMember()]
        private SortedList<string, Language> languages;

        /// <summary>
        /// Gets the language definitions (their syntactic rules and the styles assigned to the language elements)
        /// </summary>
        public SortedList<string, Language> Languages
        {
            get { return this.languages; }
        }

        /// <summary>
        /// Initializes a new instance of the Colorizer class with an empty language definition data set
        /// </summary>
        /// <remarks>
        /// Required for the deserialization
        /// </remarks>
        public Colorizer()
        {
            languages = new SortedList<string,Language>();
        }

        /// <summary>
        /// Syntax highlights the given source code using the rules and styles of the specified programming language
        /// </summary>
        /// <param name="textformatter">The formatter to be called back to apply the appropriate formatting to a specified range of the text</param>
        /// <param name="language">The programming language in which the text is written</param>
        /// <param name="text">The source code to be syntax highlighted</param>
        /// <param name="offset">Optional parameter: the position in the text where the highlightning begins</param>
        public void Colorize(TextFormatter textformatter, string language, string text, int offset = 0)
        {
            // Check for invalid or unavaliable language
            Language lang;
            try
            {
                lang = this.languages[language];
            }
            catch (KeyNotFoundException e)
            {
                throw new InvalidLanguageException(language, e);
            }
            if (lang.InvalidRegex)
            {
                throw new InvalidRegexException(language);
            }

            // Syntax higlightning using the regular expressions
            foreach (Match match in lang.MasterRegex.Matches(text))
            {
                foreach (LanguageElement element in lang.Elements)
                {
                    if (match.Groups[element.Name].Success)
                    {
                        // callback to the formatter for every language element found
                        textformatter.ColorRangeOfText(match.Index + offset, match.Length, element.Color, element.CharFormat);

                        // Recursive call if it finds a code block written in a different programming language
                        if (element.SubLanguage != null)
                            this.Colorize(textformatter, element.SubLanguage.Name, text.Substring(match.Index, match.Length), match.Index);
                    }
                }
            }                    
        }

        /// <summary>
        /// Initializes the colorizer language definitions by creating all the MasterRegex-es that represent the syntactic rules of the language
        /// </summary>
        public void Initialize()
        {
            foreach (KeyValuePair<string, Language> language in this.languages)
                language.Value.Initialize(); 
        }

        /// <summary>
        /// Fills up the language definitions and their styles with the default values (it includes 14 popular programming languages)
        /// If the Colorizer already has a language definition database then it gets replaced
        /// </summary>
        public void LoadPredefinedLanguages()
        {
            this.languages = new SortedList<string, Language>(); 
            Language lang;

            // it simplifies everything if the @"" notation is used every time consistenly

            string c_like_comment = @"/\*.*?\*/|//.*?(?=\r|\n|$)"; // '.' can be newline
            string c_like_string = @"""([^""\\]|\\.)*""|'([^'\\]|\\.)*'";  
            string c_like_number = @"-?[0-9](?:[eE]-|[\w\.])*"; // TODO: "var -66"
            
            //C#

            string CSharp_keywords = "abstract as base bool break byte case catch char "
                + "checked class const continue decimal default delegate do double else "
                + "enum event explicit extern false finally fixed float for foreach get goto "
                + "if implicit in int interface internal is lock long namespace new null "
                + "object operator out override partial params private protected public "
                + "readonly ref return sbyte sealed short sizeof stackalloc static string "
                + "set struct switch this throw true try typeof uint ulong unchecked unsafe "
                + "ushort using value virtual void volatile where while yield";

            string CSharp_preprocessor = @"#\s*if #\s*else #\s*elif #\s*endif #\s*define #\s*undef "
                + @"#\s*warning  #\s*error #\s*line #\s*region #\s*endregion #\s*pragma";
            string csharp_string1 = @"(?<!"")""([^""\\]|\\.)*""|'([^'\\]|\\.)*'";
            string csharp_string2 = @"@"".*?""|(?<="")"".*?"""; 

            lang = new Language("C#");
            lang.Elements.Add(new LanguageElement("keywords", Color.Blue, true, CSharp_keywords, @"^ \W", @"$ \W"));
            lang.Elements.Add(new LanguageElement("preprocessors", Color.Blue, true, CSharp_preprocessor, @"^ \s", @"\s $"));
            lang.Elements.Add(new LanguageElement("string", Color.DarkRed, false, csharp_string1, "", ""));
            lang.Elements.Add(new LanguageElement("string2", Color.DarkRed, false, csharp_string2, "", ""));
            lang.Elements.Add(new LanguageElement("comment", Color.Green, false, c_like_comment, "", ""));
            lang.Elements.Add(new LanguageElement("numbers", Color.FromArgb(170, 55, 0), false, c_like_number, @"^ \W", @"$ \W"));
            languages.Add("C#", lang);

            //C++

            string cpp_keywords = "auto break case catch class const decltype __finally __exception __try " +
						"const_cast continue private public protected __declspec " +
						"default delete deprecated dllexport dllimport do dynamic_cast " +
						"else enum explicit extern if for friend goto inline " +
						"mutable naked namespace new noinline noreturn nothrow " +
						"register reinterpret_cast return selectany " +
						"sizeof static static_cast struct switch template this " +
						"thread throw true false try typedef typeid typename union " +
						"using uuid virtual void volatile whcar_t while";
            string cpp_datatypes = "char bool short int __int32 __int64 __int8 __int16 long float double __wchar_t " +
						"clock_t _complex _dev_t _diskfree_t div_t ldiv_t _exception _EXCEPTION_POINTERS " +
						"FILE _finddata_t _finddatai64_t _wfinddata_t _wfinddatai64_t __finddata64_t " +
						"__wfinddata64_t _FPIEEE_RECORD fpos_t _HEAPINFO _HFILE lconv intptr_t " +
						"jmp_buf mbstate_t _off_t _onexit_t _PNH ptrdiff_t _purecall_handler " +
						"sig_atomic_t size_t _stat __stat64 _stati64 terminate_function " +
						"time_t __time64_t _timeb __timeb64 tm uintptr_t _utimbuf " +
						"va_list wchar_t wctrans_t wctype_t wint_t signed";
            string cpp_functions = "assert isalnum isalpha iscntrl isdigit isgraph islower isprint" +
						"ispunct isspace isupper isxdigit tolower toupper errno localeconv " +
						"setlocale acos asin atan atan2 ceil cos cosh exp fabs floor fmod " +
						"frexp ldexp log log10 modf pow sin sinh sqrt tan tanh jmp_buf " +
						"longjmp setjmp raise signal sig_atomic_t va_arg va_end va_start " +
						"clearerr fclose feof ferror fflush fgetc fgetpos fgets fopen " +
						"fprintf fputc fputs fread freopen fscanf fseek fsetpos ftell " +
						"fwrite getc getchar gets perror printf putc putchar puts remove " +
						"rename rewind scanf setbuf setvbuf sprintf sscanf tmpfile tmpnam " +
						"ungetc vfprintf vprintf vsprintf abort abs atexit atof atoi atol " +
						"bsearch calloc div exit free getenv labs ldiv malloc mblen mbstowcs " +
						"mbtowc qsort rand realloc srand strtod strtol strtoul system " +
						"wcstombs wctomb memchr memcmp memcpy memmove memset strcat strchr " +
						"strcmp strcoll strcpy strcspn strerror strlen strncat strncmp " +
						"strncpy strpbrk strrchr strspn strstr strtok strxfrm asctime " +
						"clock ctime difftime gmtime localtime mktime strftime time";

            string cpp_preprocessor = @"#\s*\w+";
            string cpp_include_tags = @"<.*?>";

            lang = new Language("C++");
            lang.Elements.Add(new LanguageElement("keywords", Color.Blue, true, cpp_keywords, @"^ \W", @"$ \W"));
            lang.Elements.Add(new LanguageElement("datatypes", Color.Blue, true, cpp_datatypes, @"^ \W", @"$ \W"));
            lang.Elements.Add(new LanguageElement("functions", Color.FromArgb(43, 145, 200), true, cpp_functions, @"^ \W", @"\("));
            lang.Elements.Add(new LanguageElement("preprocessors", Color.Blue, false, cpp_preprocessor, @"^ \s", @"\s $"));
            lang.Elements.Add(new LanguageElement("include_tag", Color.DarkRed, false, cpp_include_tags, @"#include\s*", ""));
            lang.Elements.Add(new LanguageElement("string", Color.DarkRed, false, c_like_string, "", ""));
            lang.Elements.Add(new LanguageElement("comment", Color.Green, false, c_like_comment, "", ""));
            lang.Elements.Add(new LanguageElement("numbers", Color.FromArgb(170, 55, 0), false, c_like_number, @"^ \W", @"$ \W"));
            languages.Add("C++", lang);

            //C

            string c_keywords = "if else switch case default break goto return for while do continue typedef sizeof NULL " +
                "void struct union enum char short int long double float signed unsigned const static extern auto register volatile " +
                "FILE size_t time_t ";
            string c_functions = "asctime clock ctime difftime gmtime localtime mktime strftime time memchr memcmp memcpy " +
                "memmove memset strcat strncat strchr strcmp strncmp strcoll strcpy strncpy strcspn strerror strlen strpbrk " +
                "strrchr strspn strstr strtok strxfrm atof atoi atol strtod strtol strtoul calloc free malloc realloc abort " +
                "atexit exit getenv system bsearch qsort abs div labs ldiv rand srand mblen mbstowcs mbtowc wcstombs wctomb " +
                "perror fgetc fgets fputcfputs getc getchar gets putc putchar puts ungetc printf fprintf sprintf scanf sscanf " +
                "fscanf clearerr fclose feof ferror fflush fgetpos fopen fread freopen fseek fsetpos ftell fwrite remove rename " +
                "rewind setbuf setvbuf tmpfile tmpnam assert acos asin atan atan2 cos cosh sin sinh tan tanh exp frexp ldexp log " +
                "log10 modf pow sqrt ceil fabs floor fmod";

            lang = new Language("C");
            lang.Elements.Add(new LanguageElement("keywords", Color.Blue, true, c_keywords, @"^ \W", @"$ \W"));
            lang.Elements.Add(new LanguageElement("preprocessors", Color.Blue, false, cpp_preprocessor, @"^ \s", @"\s $"));
            lang.Elements.Add(new LanguageElement("functions", Color.FromArgb(43,145,200), true, c_functions, @"^ \W", @"\("));
            lang.Elements.Add(new LanguageElement("include_tag", Color.DarkRed, false, cpp_include_tags, @"#include\s*", ""));
            lang.Elements.Add(new LanguageElement("string", Color.DarkRed, false, c_like_string, "", ""));
            lang.Elements.Add(new LanguageElement("comment", Color.Green, false, c_like_comment, "", ""));
            lang.Elements.Add(new LanguageElement("numbers", Color.FromArgb(170, 55, 0), false, c_like_number, @"^ \W", @"$ \W"));
            languages.Add("C", lang);

            //Java

            string java_types = "package transient strictfp void char short int long double float const static volatile byte " 
                + "boolean class interface native private protected public final abstract synchronized enum";
            string java_keywords = "instanceof assert if else switch case default break goto return for while do continue "
                + "new throw throws try catch finally this super extends implements import true false null";
            string java_annotation = @"@\w*";
            // /** ... */ comments are not treated differently

            lang = new Language("Java");
            lang.Elements.Add(new LanguageElement("keywords", Color.Blue, true, java_keywords, @"^ \W", @"$ \W"));
            lang.Elements.Add(new LanguageElement("types", Color.Blue, true, java_types, @"^ \W", @"$ \W"));
            lang.Elements.Add(new LanguageElement("annotation", Color.SeaGreen, false, java_annotation, @"^ \W", @"$ \W"));
            lang.Elements.Add(new LanguageElement("string", Color.DarkRed, false, c_like_string, "", ""));
            lang.Elements.Add(new LanguageElement("comment", Color.Green, false, c_like_comment, "", ""));
            lang.Elements.Add(new LanguageElement("numbers", Color.FromArgb(170, 55, 0), false, c_like_number, @"^ \W", @"$ \W"));
            languages.Add("Java", lang);

            //JavaScript

            string js_keywords = "break export return case for switch comment this continue if typeof "
                + "default	import var delete in void do label while else new with abstract implements protected "
                + "boolean instanceOf public byte int short char interface static double long synchronized "
                + "false native throws final null transient float package true goto private "
                + "catch enum throw class extends try const finally debugger super";

            lang = new Language("JavaScript");
            lang.Elements.Add(new LanguageElement("function_keyword", Color.Blue, false, "function", @"^ \W", @"$ \W"));
            lang.Elements[0].CharFormat.Bold = true;
            lang.Elements.Add(new LanguageElement("keywords", Color.Blue, true, js_keywords, @"^ \W", @"$ \W"));
            lang.Elements.Add(new LanguageElement("string", Color.DarkRed, false, c_like_string, "", ""));
            lang.Elements.Add(new LanguageElement("comment", Color.Green, false, c_like_comment, "", ""));
            languages.Add("JavaScript", lang);

            //Bash

            string bash_commands = "alias apropos awk basename bash bc bg builtin bzip2 cal cat cd cfdisk chgrp chmod chown chroot " +
                        "cksum clear cmp comm command cp cron crontab csplit cut date dc dd ddrescue declare df " +
                        "diff diff3 dig dir dircolors dirname dirs du echo egrep eject enable env ethtool eval " +
                        "exec exit expand export expr false fdformat fdisk fg fgrep file find fmt fold format " +
                        "free fsck ftp gawk getopts grep groups gzip hash head history id ifconfig " +
                        "import install join kill less let ln local locate logname logout look lpc lpr lprint " +
                        "lprintd lprintq lprm ls lsof make man mkdir mkfifo mkisofs mknod more mount mtools " +
                        "mv netstat nice nl nohup nslookup open op passwd paste pathchk ping popd pr printcap " +
                        "printenv printf ps pushd pwd quota quotacheck quotactl ram rcp read readonly renice " +
                        "remsync rm rmdir rsync screen scp sdiff sed select seq set sftp shift shopt shutdown " +
                        "sleep sort source split ssh strace su sudo sum symlink sync tail tar tee test time " +
                        "times touch top traceroute trap tr true tsort tty type ulimit umask umount unalias " +
                        "uname unexpand uniq units unset unshar useradd usermod users uuencode uudecode v vdir ";
						
            string bash_keywords = "vi watch wc whereis which who whoami Wget xargs yes if fi then elif else for " +
                "do done until while break continue case esac function return in eq ne ge le";

            string bash_comment = @"(?<!{)#.*?(?=\r|\n|$)";
            string bash_variable = @"\$\w+?";
            string bash_specialvar = @"\$\d \$# \$@ \$\? \$! \$\$ \$IFS";

            lang = new Language("Bash");
            lang.Elements.Add(new LanguageElement("commands", Color.Black, true, bash_commands, @"^ \W", @"$ \W"));
            lang.Elements[0].CharFormat.Bold = true;
            lang.Elements.Add(new LanguageElement("keywords", Color.Blue, true, bash_keywords, @"^ \W", @"$ \W"));
            lang.Elements.Add(new LanguageElement("special_variables", Color.BlueViolet, true, bash_specialvar, @"^ \W", @"$ \W"));
            lang.Elements.Add(new LanguageElement("string", Color.DarkRed, false, c_like_string, "", ""));
            lang.Elements.Add(new LanguageElement("comment", Color.Green, false, bash_comment, "", ""));
            lang.Elements.Add(new LanguageElement("variable", Color.BlueViolet, false, bash_variable, @"^ \W", @"$ \W"));
            languages.Add("Bash", lang);

            //SQL

            string sql_keywords = "abs absolute access acos add add_months adddate admin after aggregate all allocate alter " 
                + "and any app_name are array as asc ascii asin assertion at atan atn2 audit authid authorization "
                + "autonomous_transaction avg before begin benchmark between bfilename bin binary binary_checksum binary_integer "
                + "bit bit_count bit_and bit_or blob body boolean both breadth bulk by call cascade cascaded case cast catalog ceil "
                + "ceiling char char_base character charindex chartorowid check checksum checksum_agg chr class clob close cluster "
                + "coalesce col_length col_name collate collation collect column comment commit completion compress concat concat_ws "
                + "connect connection constant constraint constraints constructorcreate contains containsable continue conv convert "
                + "corr corresponding cos cot count count_big covar_pop covar_samp create cross cube cume_dist current current_date "
                + "current_path current_role current_time current_timestamp current_user currval cursor cycle data datalength "
                + "databasepropertyex date date_add date_format date_sub dateadd datediff datename datepart day db_id db_name deallocate "
                + "dec declare decimal decode default deferrable deferred degrees delete dense_rank depth deref desc describe descriptor "
                + "destroy destructor deterministic diagnostics dictionary disconnect difference distinct do domain double drop dump dynamic "
                + "each else elsif empth encode encrypt end end-exec equals escape every except exception exclusive exec execute exists exit exp "
                + "export_set extends external extract false fetch first first_value file float floor file_id file_name filegroup_id filegroup_name "
                + "filegroupproperty fileproperty for forall foreign format formatmessage found freetexttable from from_days fulltextcatalog fulltextservice "
                + "function general get get_lock getdate getansinull getutcdate global go goto grant greatest group grouping having heap hex hextoraw host "
                + "host_id host_name hour ident_incr ident_seed ident_current identified identity if ifnull ignore immediate in increment index index_col "
                + "indexproperty indicator initcap initial initialize initially inner inout input insert instr instrb int integer interface intersect "
                + "interval into is is_member is_srvrolemember is_null is_numeric isdate isnull isolation iterate java join key lag language large "
                + "last last_day last_value lateral lcase lead leading least left len length lengthb less level like limit limited ln lpad local localtime "
                + "localtimestamp locator lock log log10 long loop lower ltrim make_ref map match max maxextents mid min minus minute mlslabel mod mode "
                + "modifies modify module month months_between names national natural naturaln nchar nclob new new_time newid next next_day nextval no noaudit "
                + "nocompress nocopy none not nowait null nullif number number_base numeric nvl nvl2 object object_id object_name object_property ocirowid oct "
                + "of off offline old on online only opaque open operator operation option or ord order ordinalityorganization others out outer output package "
                + "pad parameter parameters partial partition path pctfree percent_rank pi pls_integer positive positiven postfix pow power pragma precision "
                + "prefix preorder prepare preserve primary prior private privileges procedure public radians raise rand range rank ratio_to_export raw rawtohex "
                + "read reads real record recursive ref references referencing reftohex relative release release_lock rename repeat replace resource restrict "
                + "result return returns reverse revoke right rollback rollup round routine row row_number rowid rowidtochar rowlabel rownum rows rowtype rpad "
                + "rtrim savepoint schema scroll scope search second section seddev_samp select separate sequence session session_user set sets share sign sin "
                + "sinh size smallint some soundex space specific specifictype sql sqlcode sqlerrm sqlexception sqlstate sqlwarning sqrt start state statement "
                + "static std stddev stdev_pop strcmp structure subdate substr substrb substring substring_index subtype successful sum synonym sys_context "
                + "sys_guid sysdate system_user table tan tanh temporary terminate than then time timestamp timezone_abbr timezone_minute timezone_hour "
                + "timezone_region to to_char to_date to_days to_number to_single_byte trailing transaction translate translation treat trigger trim "
                + "true trunc truncate type ucase uid under union unique unknown unnest update upper usage use user userenv using validate value values "
                + "var_pop var_samp varchar varchar2 variable variance varying view vsize when whenever where with without while with work write year zone";

            string sql_comments = @"--.*?(?=\r|\n|$)";
            string sql_load = @"@.*?(?=\r|\n)"; // oracle only?

            lang = new Language("SQL");
            lang.Elements.Add(new LanguageElement("keywords", Color.Blue, true, sql_keywords, @"^ \W", @"$ \W"));
            lang.Elements.Add(new LanguageElement("string", Color.DarkRed, false, c_like_string, "", "")); 
            lang.Elements.Add(new LanguageElement("load", Color.DarkRed, false, sql_load, "", ""));
            lang.Elements.Add(new LanguageElement("comment", Color.Green, false, sql_comments, "", ""));
            lang.CaseSensitive = false;
            languages.Add("SQL", lang);

            //Visual Basic

            string vb_comment1 = @"'.*?(?=\r|\n|$)";
            string vb_comment2 = @"REM\s.*?(?=\r|\n|$)";
            string vb_string = @""".*?"""; // can't use C-like string here, cause ' has a different meaning
            string vb_preprocessor = @"#\s*Const #\s*If #\s*Else #\s*ElseIf #\s*End\s+If #\s*ExternalSource " +
                                  @"#\s*End\s+ExternalSource #\s*Region #\s*End\s+Region";
            string vb_keywords = "AddHandler AddressOf AndAlso Alias And Ansi As Assembly Auto Boolean ByRef "
                + "Byte ByVal Call Case Catch CBool CByte CChar CDate CDec CDbl Char CInt Class CLng CObj Const "
                + "CShort CSng CStr CType Date Decimal Declare Default Delegate Dim DirectCast Do Double Each "
                + "Else ElseIf End Enum Erase Error Event Exit False Finally For Friend Function Get GetType GoSub "
                + "GoTo Handles If Implements Imports In Inherits Integer Interface Is Let Lib Like Long Loop Me "
                + "Mod Module MustInherit MustOverride MyBase MyClass Namespace New Next Not Nothing NotInheritable "
                + "NotOverridable Object On Option Optional Or OrElse Overloads Overridable Overrides ParamArray "
                + "Preserve Private Property Protected Public RaiseEvent ReadOnly ReDim RemoveHandler Resume "
                + "Return Select Set Shadows Shared Short Single Static Step Stop String Structure Sub SyncLock Then "
                + "Throw To True Try TypeOf Unicode Until Variant When While With WithEvents WriteOnly Xor";

            lang = new Language("Visual Basic");
            lang.Elements.Add(new LanguageElement("keywords", Color.Blue, true, vb_keywords, @"^ \W", @"$ \W"));
            lang.Elements.Add(new LanguageElement("preprocessor", Color.Blue, true, vb_preprocessor, @"^ \W", @"$ \W"));
            lang.Elements.Add(new LanguageElement("string", Color.DarkRed, false, vb_string, "", ""));
            lang.Elements.Add(new LanguageElement("comment1", Color.Green, false, vb_comment1, "", ""));
            lang.Elements.Add(new LanguageElement("comment2", Color.Green, false, vb_comment2, "", ""));
            languages.Add("Visual Basic", lang);

            //Assembly (AVR)

            string assembly_keywords = 
                "ADD ADC ADIW SUB SUBI SBC SBCI SBIW AND ANDI OR ORI EOR COM NEG SBR CBR INC DEC TST CLR SER MUL " 
                + "RJMP IJMP JMP RCALL ICALL CALL RET RETI CPSE CP CPC CPI SBRC SBRS SBIC SBIS BRBS BRBC"
                + "BREQ BRNE BRCS BRCC BRSH BRLO BRMI BRPL BRGE BRLT BRHS BRHC BRTS BRTC BRVS BRVC BRIE BRID"
                + "MOV LDI LDS LD LDD STS ST STD LPM IN OUT PUSH POP LSL LSR ROL ROR ASR SWAP BSET BCLR SBI CBI"
                + "BST BLD SEC CLC SEN CLN SEZ CLZ SEI CLI SES CLS SEV CLV SET CLT SEH CLH NOP SLEEP WDR";

            string assembly_comment = @";.*?(?=\r|\n|$)";

            string assembly_registers = @"r\d\d?"; 

            lang = new Language("Assembly");
            lang.Elements.Add(new LanguageElement("keywords", Color.Blue, true, assembly_keywords, @"^ \W", @"$ \W"));
            lang.Elements.Add(new LanguageElement("registers", Color.BlueViolet, false, assembly_registers, @"^ \W", @"$ \W"));
            lang.Elements.Add(new LanguageElement("string", Color.DarkRed, false, c_like_string, "", "")); // for include
            lang.Elements.Add(new LanguageElement("comment", Color.Green, false, assembly_comment, "", ""));
            lang.CaseSensitive = false;
            languages.Add("Assembly", lang);

            //Matlab

            string matlab_keywords = "break case catch continue else elseif end for function global if otherwise persistent "
                + "return switch try while classdef methods properties";

            string matlab_comment = @"%.*?(?=\r|\n|$)";

            string matlab_string = @"'.*?'";

            lang = new Language("Matlab");
            lang.Elements.Add(new LanguageElement("keywords", Color.Blue, true, matlab_keywords, @"^ \W", @"$ \W"));
            lang.Elements.Add(new LanguageElement("string", Color.DarkRed, false, matlab_string, "", ""));
            lang.Elements.Add(new LanguageElement("comment", Color.Green, false, matlab_comment, "", ""));
            languages.Add("Matlab", lang);

            //XML

            string xml_comment = @"<!--.*?-->";
            string xml_string1 = @"(?<!>[^<]*?)(?<=(=\s*))"".*?""";
            string xml_string2 = @"(?<!>[^<]*?)(?<=(=\s*))'.*?'";
            string xml_attr = @"(?<!>[^<]*?)(?<=\s)[\w:\.\-]*?(?=\s*=\s*(""|'))";
            string xml_tag = @"(<(/|\?)?[\w\-:\.]*?(\s|/?>))|((/|\?)?>)";
            string xml_cdata = @"<\!\[[\w\s]*?\[.*?\]\]>";

            lang = new Language("XML");
            lang.Elements.Add(new LanguageElement("attribute", Color.Red, false, xml_attr, "", ""));
            lang.Elements.Add(new LanguageElement("string1", Color.Blue, false, xml_string1, "", ""));
            lang.Elements.Add(new LanguageElement("string2", Color.Blue, false, xml_string2, "", ""));
            lang.Elements.Add(new LanguageElement("comment", Color.Green, false, xml_comment, "", ""));
            lang.Elements.Add(new LanguageElement("xml_tag", Color.DarkRed, false, xml_tag, "", ""));
            lang.Elements.Add(new LanguageElement("xml_cdata", Color.DarkGray, false, xml_cdata, "", ""));
            languages.Add("XML", lang);

            //HTML

            string html_doctype = @"<!DOCTYPE.*?>";

            lang = new Language("HTML");
            lang.Elements.Add(new LanguageElement("attribute", Color.DarkOrange, false, xml_attr, "", ""));
            lang.Elements.Add(new LanguageElement("string1", Color.Blue, false, xml_string1, "", ""));
            lang.Elements.Add(new LanguageElement("string2", Color.Blue, false, xml_string2, "", ""));
            lang.Elements.Add(new LanguageElement("comment", Color.Green, false, xml_comment, "", ""));
            lang.Elements.Add(new LanguageElement("tag", Color.Purple, false, xml_tag, "", ""));
            lang.Elements.Add(new LanguageElement("doctype", Color.DarkGray, false, html_doctype, "", ""));
            languages.Add("HTML", lang);

            //PHP

		    string php_keywords = "abstract and array as break case catch cfunction class clone const continue declare default die do " +
						"else elseif enddeclare endfor endforeach endif endswitch endwhile extends final for foreach " +
						"function global goto if implements include include_once interface instanceof insteadof namespace new " +
						"old_function or private protected public return require require_once static switch " +
						"trait throw try use var while xor __FILE__ __LINE__ __METHOD__ __FUNCTION__ __CLASS__ TRUE FALSE true false";

            string php_functions = "Wabs acos acosh addcslashes addslashes " +
						"array_change_key_case array_chunk array_combine array_count_values array_diff " +
						"array_diff_assoc array_diff_key array_diff_uassoc array_diff_ukey array_fill " +
						"array_filter array_flip array_intersect array_intersect_assoc array_intersect_key " +
						"array_intersect_uassoc array_intersect_ukey array_key_exists array_keys array_map " +
						"array_merge array_merge_recursive array_multisort array_pad array_pop array_product " +
						"array_push array_rand array_reduce array_reverse array_search array_shift " +
						"array_slice array_splice array_sum array_udiff array_udiff_assoc " +
						"array_udiff_uassoc array_uintersect array_uintersect_assoc " +
						"array_uintersect_uassoc array_unique array_unshift array_values array_walk " +
						"array_walk_recursive atan atan2 atanh base64_decode base64_encode base_convert " +
						"basename bcadd bccomp bcdiv bcmod bcmul bindec bindtextdomain bzclose bzcompress " +
						"bzdecompress bzerrno bzerror bzerrstr bzflush bzopen bzread bzwrite ceil chdir " +
						"checkdate checkdnsrr chgrp chmod chop chown chr chroot chunk_split class_exists " +
						"closedir closelog copy cos cosh count count_chars date decbin dechex decoct " +
						"deg2rad delete ebcdic2ascii echo empty end ereg ereg_replace eregi eregi_replace error_log " +
						"error_reporting escapeshellarg escapeshellcmd eval exec exit exp explode extension_loaded " +
						"feof fflush fgetc fgetcsv fgets fgetss file_exists file_get_contents file_put_contents " +
						"fileatime filectime filegroup fileinode filemtime fileowner fileperms filesize filetype " +
						"floatval flock floor flush fmod fnmatch fopen fpassthru fprintf fputcsv fputs fread fscanf " +
						"fseek fsockopen fstat ftell ftok getallheaders getcwd getdate getenv gethostbyaddr gethostbyname " +
						"gethostbynamel getimagesize getlastmod getmxrr getmygid getmyinode getmypid getmyuid getopt " +
						"getprotobyname getprotobynumber getrandmax getrusage getservbyname getservbyport gettext " +
						"gettimeofday gettype glob gmdate gmmktime ini_alter ini_get ini_get_all ini_restore ini_set " +
						"interface_exists intval ip2long is_a is_array is_bool is_callable is_dir is_double " +
						"is_executable is_file is_finite is_float is_infinite is_int is_integer is_link is_long " +
						"is_nan is_null is_numeric is_object is_readable is_real is_resource is_scalar is_soap_fault " +
						"is_string is_subclass_of is_uploaded_file is_writable is_writeable mkdir mktime nl2br " +
						"parse_ini_file parse_str parse_url passthru pathinfo print readlink realpath rewind rewinddir rmdir " +
						"round str_ireplace str_pad str_repeat str_replace str_rot13 str_shuffle str_split " +
						"str_word_count strcasecmp strchr strcmp strcoll strcspn strftime strip_tags stripcslashes " +
						"stripos stripslashes stristr strlen strnatcasecmp strnatcmp strncasecmp strncmp strpbrk " +
						"strpos strptime strrchr strrev strripos strrpos strspn strstr strtok strtolower strtotime " +
						"strtoupper strtr strval substr substr_compare";

            string php_comment = @"#.*?(?=\r|\n|$)";
            string php_variable = @"\$\w+";
            string php_heradocstring = @"<<<""?(?<heradocid>.*?(?=""?(?:\r|\n|$))).*?\k<heradocid>";
            string php_meta = @"<\?php|\?>";

            lang = new Language("PHP");
            lang.Elements.Add(new LanguageElement("keywords", Color.Blue, true, php_keywords, @"^ \W", @"$ \W"));
            lang.Elements.Add(new LanguageElement("functions", Color.FromArgb(43, 145, 200), true, php_functions, @"^ \W", @"$ \W"));
            lang.Elements.Add(new LanguageElement("variable", Color.Purple, false, php_variable, @"^ \W", @"$ \W"));
            lang.Elements.Add(new LanguageElement("string", Color.DarkRed, false, c_like_string, "", ""));
            lang.Elements.Add(new LanguageElement("string_heradoc", Color.DarkRed, false, php_heradocstring, "", ""));
            lang.Elements.Add(new LanguageElement("comment", Color.Green, false, c_like_comment, "", ""));
            lang.Elements.Add(new LanguageElement("comment2", Color.Green, false, php_comment, "", ""));
            lang.Elements.Add(new LanguageElement("meta", Color.Gray, false, php_meta, "", ""));
            languages.Add("PHP", lang);

            //CSS

            string css_keywords = "ascent azimuth background-attachment background-color background-image background-position " +
						"background-repeat background baseline bbox border-collapse border-color border-spacing border-style border-top " +
						"border-right border-bottom border-left border-top-color border-radius border-right-color border-bottom-color border-left-color " +
						"border-top-style border-right-style border-bottom-style border-left-style border-top-width border-right-width " +
						"border-bottom-width border-left-width border-width border bottom cap-height caption-side centerline clear clip color " +
						"content counter-increment counter-reset cue-after cue-before cue cursor definition-src descent direction display " +
						"elevation empty-cells filter float font-size-adjust font-family font-size font-stretch font-style font-variant font-weight font " +
						"height left letter-spacing line-height list-style-image list-style-position list-style-type list-style margin-top " +
						"margin-right margin-bottom margin-left margin marker-offset marks mathline max-height max-width min-height min-width opacity orphans " +
						"outline-color outline-style outline-width outline overflow padding-top padding-right padding-bottom padding-left padding page " +
						"page-break-after page-break-before page-break-inside pause pause-after pause-before pitch pitch-range play-during position " +
						"quotes right richness size slope src speak-header speak-numeral speak-punctuation speak speech-rate stemh stemv stress " +
						"table-layout text-align top text-decoration text-indent text-shadow text-transform unicode-bidi unicode-range units-per-em " +
						"vertical-align visibility voice-family volume white-space widows width widths word-spacing x-height z-index";

		    string css_values =	"above absolute all always aqua armenian attr aural auto avoid baseline behind below bidi-override black blink block blue bold bolder " +
						"both bottom braille capitalize caption center center-left center-right circle close-quote code collapse compact condensed " +
						"continuous counter counters crop cross crosshair cursive dashed decimal decimal-leading-zero default digits disc dotted double " +
						"embed embossed e-resize expanded extra-condensed extra-expanded fantasy far-left far-right fast faster fixed format fuchsia " +
                        "gray green groove handheld hebrew help hidden hide high higher icon inline-block inline-table inline inset inside invert italic " +
						"justify landscape large larger left-side left leftwards level lighter lime line-through list-item local loud lower-alpha " +
						"lowercase lower-greek lower-latin lower-roman lower low ltr marker maroon medium message-box middle mix move narrower " +
						"navy ne-resize no-close-quote none no-open-quote no-repeat normal nowrap n-resize nw-resize oblique olive once open-quote outset " +
						"outside overline pointer portrait pre print projection purple red relative repeat repeat-x repeat-y rgb ridge right right-side " +
						"rightwards rtl run-in screen scroll semi-condensed semi-expanded separate se-resize show silent silver slower slow " +
						"small small-caps small-caption smaller soft solid speech spell-out square s-resize static status-bar sub super sw-resize " +
						"table-caption table-cell table-column table-column-group table-footer-group table-header-group table-row table-row-group teal " +
						"text-bottom text-top thick thin top transparent tty tv ultra-condensed ultra-expanded underline upper-alpha uppercase upper-latin " +
						"upper-roman url visible wait white wider w-resize x-fast x-high x-large x-loud x-low x-slow x-small x-soft xx-large xx-small yellow";

            string css_fonts = "[mM]onospace [tT]ahoma [vV]erdana [aA]rial [hH]elvetica [sS]ans-[sS]erif [sS]an-[sS]erif [sS]erif [cC]ourier mono sans serif [gG]eneva [cC]onsolas " +
                "[gG]eorgia [pP]alatino [tT]imes [gG]adget cursive [iI]mpact [cC]harcoal [mM]onaco";

            string c_likecomment1 = @"/\*.*?\*/";
            string css_string = @"('.*?')|("".*?"")";
            string css_color = @"#[a-fA-F0-9]{3,8}";
            string css_value = @"-?\d+(\.\d+)?(?:px|em|pt|\%|)\s*?";
            string css_important = @"!important";
            string css_tag = @"[\w\.#][\w\s\.:#,\-_*]*?(?!;)";
            string css_function = @"[\w\.\-_]*?";

            lang = new Language("CSS");
            lang.Elements.Add(new LanguageElement("fonts", Color.FromArgb(43, 145, 200), true, css_fonts, @"^ \W", @""));
            lang.Elements.Add(new LanguageElement("keyword", Color.Blue, true, css_keywords, @"^ \W", @": \s"));
            lang.Elements.Add(new LanguageElement("values", Color.FromArgb(43, 145, 200), true, css_values, @"^ \W", @", ; \s } !"));
            lang.Elements.Add(new LanguageElement("attribute", Color.DeepPink, false, css_tag, @"^ \n \r } \s", @"{ /"));
            lang.Elements.Add(new LanguageElement("value_number", Color.FromArgb(43, 145, 200), false, css_value, @"^ \W", @", ; \s } \) !"));
            lang.Elements.Add(new LanguageElement("value_function", Color.FromArgb(43, 145, 200), false, css_function, @"^ \W", @"\("));
            lang.Elements.Add(new LanguageElement("string", Color.DarkRed, false, css_string, "", ""));
            lang.Elements.Add(new LanguageElement("comment", Color.Green, false, c_likecomment1, "", ""));
            lang.Elements.Add(new LanguageElement("color", Color.Purple, false, css_color, "", ""));
            lang.Elements.Add(new LanguageElement("important", Color.Red, false, css_important, "", ""));
            languages.Add("CSS", lang);


            //HTML + Js, CSS, PHP

            string html_css = @"(?<=<style(?:\s[^>]*?)?>).+?(?=</style>)";
            string html_js = @"(?<=<script(?:\s[^>]*?)?>).*?(?=</script>)";
            string html_php = @"(?<=<\?php\s*).+?(?=\?>)";

            lang = new Language("HTML + JS,CSS,PHP");
            lang.Elements.Add(new LanguageElement("attribute", Color.DarkOrange, false, xml_attr, "", ""));
            lang.Elements.Add(new LanguageElement("string1", Color.Blue, false, xml_string1, "", ""));
            lang.Elements.Add(new LanguageElement("string2", Color.Blue, false, xml_string2, "", ""));
            lang.Elements.Add(new LanguageElement("comment", Color.Green, false, xml_comment, "", ""));
            lang.Elements.Add(new LanguageElement("tag", Color.Purple, false, xml_tag, "", ""));
            lang.Elements.Add(new LanguageElement("doctype", Color.DarkGray, false, html_doctype, "", ""));

            LanguageElement e = new LanguageElement("css", Color.Black, false, html_css, "", "");
            e.SubLanguage = languages["CSS"];
            lang.Elements.Add(e);
            e = new LanguageElement("javascript", Color.Black, false, html_js, "", "");
            e.SubLanguage = languages["JavaScript"];
            lang.Elements.Add(e);
            e = new LanguageElement("php", Color.Black, false, html_php, "", "");
            e.SubLanguage = languages["PHP"];
            lang.Elements.Add(e);
            languages.Add("HTML + JS,CSS,PHP", lang);


            // Initialize all languages, so that the regexes given here are combined to a masterRegex for each language
            this.Initialize();
        }

    }
}
