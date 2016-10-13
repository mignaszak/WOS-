using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WOSApi.Model 
{
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://scientific.thomsonreuters.com/schema/wok5.4/public/FullRecord")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://scientific.thomsonreuters.com/schema/wok5.4/public/FullRecord", IsNullable = false)]
    public partial class records
    {

        private Rec[] _REC;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("REC")]
        public Rec[] REC
        {
            get
            {
                return this._REC;
            }
            set
            {
                this._REC = value;
            }
        }
    }

    public partial class Rec
    {

        private string _UID;

        private StaticData _static_data;

        private DynamicData _dynamic_data;

        /// <remarks/>
        public string UID
        {
            get
            {
                return this._UID;
            }
            set
            {
                this._UID = value;
            }
        }

        /// <remarks/>
        public StaticData static_data
        {
            get
            {
                return this._static_data;
            }
            set
            {
                this._static_data = value;
            }
        }

        /// <remarks/>
        public DynamicData dynamic_data
        {
            get
            {
                return this._dynamic_data;
            }
            set
            {
                this._dynamic_data = value;
            }
        }
    }


    public partial class StaticData
    {

        private DataSummary _summary;

        /// <remarks/>
        public DataSummary summary
        {
            get
            {
                return this._summary;
            }
            set
            {
                this._summary = value;
            }
        }
    }


    public partial class DataSummary
    {

        private DataPubInfo _pub_info;

        private DataTitles _titles;

        private DataNames _names;

        /// <remarks/>
        public DataPubInfo pub_info
        {
            get
            {
                return this._pub_info;
            }
            set
            {
                this._pub_info = value;
            }
        }

        /// <remarks/>
        public DataTitles titles
        {
            get
            {
                return this._titles;
            }
            set
            {
                this._titles = value;
            }
        }

        /// <remarks/>
        public DataNames names
        {
            get
            {
                return this._names;
            }
            set
            {
                this._names = value;
            }
        }
    }

    public partial class DataPubInfo
    {

        private DataPage _page;

        private string _issue;

        private string _pubtype;

        private System.DateTime _sortdate;

        private string _has_abstract;

        private string _coverdate;

        private string _pubmonth;

        private string _vol;

        private int _pubyear;

        /// <remarks/>
        public DataPage page
        {
            get
            {
                return this._page;
            }
            set
            {
                this._page = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string issue
        {
            get
            {
                return this._issue;
            }
            set
            {
                this._issue = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string pubtype
        {
            get
            {
                return this._pubtype;
            }
            set
            {
                this._pubtype = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "date")]
        public System.DateTime sortdate
        {
            get
            {
                return this._sortdate;
            }
            set
            {
                this._sortdate = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string has_abstract
        {
            get
            {
                return this._has_abstract;
            }
            set
            {
                this._has_abstract = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string coverdate
        {
            get
            {
                return this._coverdate;
            }
            set
            {
                this._coverdate = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string pubmonth
        {
            get
            {
                return this._pubmonth;
            }
            set
            {
                this._pubmonth = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string vol
        {
            get
            {
                return this._vol;
            }
            set
            {
                this._vol = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int pubyear
        {
            get
            {
                return this._pubyear;
            }
            set
            {
                this._pubyear = value;
            }
        }
    }


    public partial class DataPage
    {

        private string _end;

        private string _page_count;

        private string _begin;

        private string _value;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string end
        {
            get
            {
                return this._end;
            }
            set
            {
                this._end = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string page_count
        {
            get
            {
                return this._page_count;
            }
            set
            {
                this._page_count = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string begin
        {
            get
            {
                return this._begin;
            }
            set
            {
                this._begin = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this._value;
            }
            set
            {
                this._value = value;
            }
        }
    }


    public partial class DataTitles
    {

        private DataTile[] _title;

        private int countField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("title")]
        public DataTile[] title
        {
            get
            {
                return this._title;
            }
            set
            {
                this._title = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int count
        {
            get
            {
                return this.countField;
            }
            set
            {
                this.countField = value;
            }
        }
    }

    public partial class DataTile
    {

        private string _type;

        private string _value;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string type
        {
            get
            {
                return this._type;
            }
            set
            {
                this._type = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this._value;
            }
            set
            {
                this._value = value;
            }
        }
    }


    public partial class DataNames
    {

        private DataName[] _name;

        private int countField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("name")]
        public DataName[] name
        {
            get
            {
                return this._name;
            }
            set
            {
                this._name = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int count
        {
            get
            {
                return this.countField;
            }
            set
            {
                this.countField = value;
            }
        }
    }


    public partial class DataName
    {

        private string _display_name;

        private string _full_name;

        private string _wos_standard;

        private string _first_name;

        private string _last_name;

        private string _dais_id;

        private string _reprint;

        private string _daisng_id;

        private string _role;

        private string _seq_no;

        /// <remarks/>
        public string display_name
        {
            get
            {
                return this._display_name;
            }
            set
            {
                this._display_name = value;
            }
        }

        /// <remarks/>
        public string full_name
        {
            get
            {
                return this._full_name;
            }
            set
            {
                this._full_name = value;
            }
        }

        /// <remarks/>
        public string wos_standard
        {
            get
            {
                return this._wos_standard;
            }
            set
            {
                this._wos_standard = value;
            }
        }

        /// <remarks/>
        public string first_name
        {
            get
            {
                return this._first_name;
            }
            set
            {
                this._first_name = value;
            }
        }

        /// <remarks/>
        public string last_name
        {
            get
            {
                return this._last_name;
            }
            set
            {
                this._last_name = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string dais_id
        {
            get
            {
                return this._dais_id;
            }
            set
            {
                this._dais_id = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string reprint
        {
            get
            {
                return this._reprint;
            }
            set
            {
                this._reprint = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string daisng_id
        {
            get
            {
                return this._daisng_id;
            }
            set
            {
                this._daisng_id = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string role
        {
            get
            {
                return this._role;
            }
            set
            {
                this._role = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string seq_no
        {
            get
            {
                return this._seq_no;
            }
            set
            {
                this._seq_no = value;
            }
        }
    }


    public partial class DynamicData
    {

        private CitationRelated _citation_related;

        private ClusterRelated _cluster_related;

        /// <remarks/>
        public CitationRelated citation_related
        {
            get
            {
                return this._citation_related;
            }
            set
            {
                this._citation_related = value;
            }
        }

        /// <remarks/>
        public ClusterRelated cluster_related
        {
            get
            {
                return this._cluster_related;
            }
            set
            {
                this._cluster_related = value;
            }
        }
    }

    public partial class CitationRelated
    {

        private TcList _tc_list;

        /// <remarks/>
        public TcList tc_list
        {
            get
            {
                return this._tc_list;
            }
            set
            {
                this._tc_list = value;
            }
        }
    }

    public partial class TcList
    {

        private SiloTc _silo_tc;

        /// <remarks/>
        public SiloTc silo_tc
        {
            get
            {
                return this._silo_tc;
            }
            set
            {
                this._silo_tc = value;
            }
        }
    }

    public partial class SiloTc
    {

        private int _local_count;

        private string _coll_id;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int local_count
        {
            get
            {
                return this._local_count;
            }
            set
            {
                this._local_count = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string coll_id
        {
            get
            {
                return this._coll_id;
            }
            set
            {
                this._coll_id = value;
            }
        }
    }

    public partial class ClusterRelated
    {

        private Identifier[] _identifiers;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("identifier", IsNullable = false)]
        public Identifier[] identifiers
        {
            get
            {
                return this._identifiers;
            }
            set
            {
                this._identifiers = value;
            }
        }
    }

    public partial class Identifier
    {

        private string _value;

        private string _type;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string value
        {
            get
            {
                return this._value;
            }
            set
            {
                this._value = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string type
        {
            get
            {
                return this._type;
            }
            set
            {
                this._type = value;
            }
        }
    }


}
