using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using KModkit;

public class outrageousScript : MonoBehaviour {

    public KMBombInfo Bomb;
    public KMAudio Audio;

    public KMSelectable StartButton;
    public KMSelectable[] Numbers;
    public KMSelectable[] Directions; //ULDR
    public KMSelectable[] Symbols;
    public GameObject DefaultMode;
    public GameObject SubmissionMode;
    public GameObject LEDObject;
    public Material[] LEDColors; //black, red, green, white, blue

    public TextMesh Text;
    public TextMesh SingleCharacter;

    //Logging
    static int moduleIdCounter = 1;
    int moduleId;
    private bool moduleSolved;

    private string base36 = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private List<string> words = new List<string> { "ABOARD", "ABROAD", "ABSORB", "ABUSED", "ABUSES", "ACCENT", "ACCEPT", "ACCESS", "ACCORD", "ACROSS", "ACTING", "ACTION", "ACTORS", "ADDING", "ADJUST", "ADMIRE", "ADMITS", "ADULTS", "ADVENT", "ADVERT", "ADVICE", "ADVISE", "AFFAIR", "AFFECT", "AFFORD", "AFIELD", "AGEING", "AGENCY", "AGENDA", "AGENTS", "AGREED", "AGREES", "AIMING", "ALBEIT", "ALBUMS", "ALIENS", "ALLIES", "ALLOWS", "ALMOST", "ALWAYS", "AMIDST", "AMOUNT", "AMUSED", "ANCHOR", "ANGELS", "ANGLES", "ANIMAL", "ANKLES", "ANSWER", "ANYHOW", "ANYONE", "ANYWAY", "APPEAL", "APPEAR", "APPLES", "ARCHES", "ARGUED", "ARGUES", "ARISEN", "ARISES", "ARMIES", "ARMOUR", "AROUND", "ARREST", "ARRIVE", "ARROWS", "ARTERY", "ARTIST", "ASCENT", "ASHORE", "ASKING", "ASPECT", "ASSENT", "ASSERT", "ASSESS", "ASSETS", "ASSIGN", "ASSIST", "ASSUME", "ASSURE", "ASTHMA", "ASYLUM", "ATTACH", "ATTACK", "ATTAIN", "ATTEND", "AUTHOR", "AUTUMN", "AVENUE", "AVOIDS", "AWARDS", "BABIES", "BACKED", "BALLET", "BALLOT", "BANANA", "BANGED", "BANKER", "BANNED", "BANNER", "BARELY", "BARLEY", "BARMAN", "BARONS", "BARREL", "BASICS", "BASINS", "BASKET", "BATTLE", "BEASTS", "BEATEN", "BEAUTY", "BECAME", "BECOME", "BEFORE", "BEGGED", "BEGINS", "BEHALF", "BEHAVE", "BEHIND", "BEINGS", "BELIEF", "BELONG", "BESIDE", "BETTER", "BEWARE", "BEYOND", "BIDDER", "BIGGER", "BIOPSY", "BIRTHS", "BISHOP", "BITING", "BITTEN", "BLACKS", "BLADES", "BLAMED", "BLOCKS", "BLOKES", "BLOODY", "BLOUSE", "BOARDS", "BOASTS", "BODIES", "BOILER", "BOLDLY", "BOMBER", "BONNET", "BOOKED", "BORDER", "BORROW", "BOSSES", "BOTHER", "BOTTLE", "BOTTOM", "BOUGHT", "BOUNDS", "BOWLER", "BOXING", "BRAINS", "BRAKES", "BRANCH", "BRANDS", "BRANDY", "BREACH", "BREAKS", "BREAST", "BREATH", "BREEDS", "BREEZE", "BRICKS", "BRIDGE", "BRINGS", "BROKEN", "BROKER", "BRONZE", "BUBBLE", "BUCKET", "BUDGET", "BUFFER", "BUFFET", "BUGGER", "BUILDS", "BULLET", "BUNDLE", "BURDEN", "BUREAU", "BURIAL", "BURIED", "BURNED", "BURROW", "BURSTS", "BUSHES", "BUTLER", "BUTTER", "BUTTON", "BUYERS", "BUYING", "BYPASS", "CABLES", "CALLED", "CALLER", "CALMLY", "CALVES", "CAMERA", "CAMPUS", "CANALS", "CANCEL", "CANCER", "CANDLE", "CANNON", "CANOPY", "CANVAS", "CARBON", "CAREER", "CARERS", "CARING", "CARPET", "CARROT", "CARVED", "CASTLE", "CATTLE", "CAUGHT", "CAUSED", "CAUSES", "CAVITY", "CEASED", "CELLAR", "CEMENT", "CENSUS", "CENTER", "CENTRE", "CEREAL", "CHAINS", "CHAIRS", "CHANCE", "CHANGE", "CHAPEL", "CHARGE", "CHARTS", "CHECKS", "CHEEKS", "CHEERS", "CHEESE", "CHEQUE", "CHERRY", "CHICKS", "CHIEFS", "CHOICE", "CHOOSE", "CHORDS", "CHORUS", "CHOSEN", "CHUNKS", "CHURCH", "CINEMA", "CIRCLE", "CIRCUS", "CITIES", "CITING", "CLAIMS", "CLAUSE", "CLERGY", "CLERKS", "CLIENT", "CLIFFS", "CLIMAX", "CLINIC", "CLOCKS", "CLONES", "CLOSED", "CLOSER", "CLOSES", "CLOUDS", "CLUTCH", "COASTS", "COFFEE", "COFFIN", "COHORT", "COLDER", "COLDLY", "COLLAR", "COLONY", "COLOUR", "COLUMN", "COMBAT", "COMEDY", "COMING", "COMMIT", "COMPLY", "CONVEY", "CONVOY", "COOKED", "COOKER", "COOLER", "COOLLY", "COPIED", "COPIES", "COPING", "COPPER", "CORNER", "CORPSE", "CORPUS", "CORTEX", "COTTON", "COUNTS", "COUNTY", "COUPLE", "COUPON", "COURSE", "COURTS", "COUSIN", "COVERS", "CRACKS", "CRADLE", "CREATE", "CREDIT", "CRIMES", "CRISES", "CRISIS", "CRISPS", "CRITIC", "CROWDS", "CRUISE", "CRYING", "CUCKOO", "CURLED", "CURSED", "CURSOR", "CURVES", "CUSTOM", "CUTTER", "CYCLES", "DAMAGE", "DANCED", "DANCER", "DANCES", "DANGER", "DARING", "DARKER", "DASHED", "DATING", "DEALER", "DEARLY", "DEATHS", "DEBATE", "DEBRIS", "DEBTOR", "DECADE", "DECIDE", "DECREE", "DEEMED", "DEEPER", "DEEPLY", "DEFEAT", "DEFECT", "DEFEND", "DEFINE", "DEGREE", "DELAYS", "DEMAND", "DEMISE", "DEMONS", "DENIAL", "DENIED", "DENIES", "DEPEND", "DEPTHS", "DEPUTY", "DERIVE", "DESERT", "DESIGN", "DESIRE", "DETAIL", "DETECT", "DEVICE", "DEVISE", "DEVOTE", "DIESEL", "DIFFER", "DIGEST", "DIGITS", "DINGHY", "DINING", "DINNER", "DIRECT", "DISHES", "DISMAY", "DIVERS", "DIVERT", "DIVIDE", "DIVING", "DOCTOR", "DOLLAR", "DOMAIN", "DONKEY", "DONORS", "DOOMED", "DOUBLE", "DOUBLY", "DOUBTS", "DOZENS", "DRAGON", "DRAINS", "DRAWER", "DREAMS", "DRINKS", "DRIVEN", "DRIVER", "DRIVES", "DRYING", "DUMPED", "DURING", "DUTIES", "EAGLES", "EARNED", "EASIER", "EASILY", "EASING", "EASTER", "EATING", "ECHOED", "ECHOES", "EDITED", "EDITOR", "EFFECT", "EFFORT", "EIGHTH", "EIGHTY", "EITHER", "ELBOWS", "ELDERS", "ELDEST", "ELEVEN", "ELITES", "EMBARK", "EMBRYO", "EMERGE", "EMPIRE", "EMPLOY", "ENABLE", "ENAMEL", "ENDING", "ENDURE", "ENERGY", "ENGAGE", "ENGINE", "ENJOYS", "ENOUGH", "ENSURE", "ENTAIL", "ENTERS", "ENTITY", "ENZYME", "EQUALS", "EQUITY", "ERODED", "ERRORS", "ESCAPE", "ESCORT", "ESSAYS", "ESTATE", "ESTEEM", "ETHICS", "EVENLY", "EVENTS", "EVOLVE", "EXCEED", "EXCEPT", "EXCESS", "EXCUSE", "EXISTS", "EXPAND", "EXPECT", "EXPERT", "EXPIRY", "EXPORT", "EXPOSE", "EXTEND", "EXTENT", "EXTRAS", "FABRIC", "FACETS", "FACING", "FACTOR", "FADING", "FAILED", "FAIRLY", "FALLEN", "FAMILY", "FAMINE", "FARMER", "FASTER", "FATHER", "FAULTS", "FAVOUR", "FEARED", "FELLOW", "FEMALE", "FENCES", "FIBRES", "FIELDS", "FIGHTS", "FIGURE", "FILLED", "FILTER", "FINALE", "FINALS", "FINELY", "FINEST", "FINGER", "FINISH", "FIRING", "FIRMLY", "FITTED", "FIXING", "FLAMES", "FLANKS", "FLATLY", "FLIGHT", "FLOCKS", "FLOODS", "FLOORS", "FLOWED", "FLOWER", "FLUIDS", "FLURRY", "FLYING", "FOLDED", "FOLDER", "FOLLOW", "FORCED", "FORCES", "FOREST", "FORGET", "FORGOT", "FORMAT", "FORMED", "FORMER", "FOSSIL", "FOSTER", "FOUGHT", "FOURTH", "FRAMES", "FRANCS", "FREELY", "FREEZE", "FRENCH", "FRENZY", "FRIDGE", "FRIEND", "FRIGHT", "FRINGE", "FRONTS", "FROZEN", "FRUITS", "FULFIL", "FULLER", "FUNDED", "FUNGUS", "FUSION", "FUTURE", "GAINED", "GALAXY", "GALLON", "GAMBLE", "GARAGE", "GARDEN", "GARLIC", "GASPED", "GATHER", "GAZING", "GEARED", "GENDER", "GENIUS", "GENTLY", "GENTRY", "GERMAN", "GHOSTS", "GIANTS", "GIVING", "GLADLY", "GLANCE", "GLANDS", "GLARED", "GLIDER", "GLOVES", "GOLFER", "GOSPEL", "GOSSIP", "GOVERN", "GRADES", "GRAINS", "GRANNY", "GRANTS", "GRAPES", "GRAPHS", "GRAVEL", "GRAVES", "GREASE", "GREENS", "GRIMLY", "GROOVE", "GROUND", "GROUPS", "GROWTH", "GUARDS", "GUESTS", "GUIDED", "GUIDES", "GUITAR", "GUNMEN", "GUTTER", "HABITS", "HALTED", "HALVES", "HAMLET", "HAMMER", "HANDED", "HANDLE", "HAPPEN", "HARDER", "HARDLY", "HASSLE", "HATRED", "HAULED", "HAVING", "HAZARD", "HEADED", "HEADER", "HEALTH", "HEARTH", "HEARTS", "HEATER", "HEAVED", "HEAVEN", "HEDGES", "HEIGHT", "HELMET", "HELPED", "HELPER", "HEROES", "HIDDEN", "HIDING", "HIGHER", "HIGHLY", "HINDER", "HISSED", "HOCKEY", "HOLDER", "HOMAGE", "HONOUR", "HOPING", "HORROR", "HORSES", "HOSTEL", "HOTELS", "HOUNDS", "HOUSED", "HOUSES", "HUGELY", "HUGGED", "HUMANS", "HUMOUR", "HUNGER", "HUNTER", "HURDLE", "IDEALS", "IGNORE", "IMAGES", "IMPACT", "IMPORT", "IMPOSE", "INCHES", "INCOME", "INDEED", "INDUCE", "INFANT", "INFLUX", "INFORM", "INJURY", "INLAND", "INPUTS", "INSECT", "INSIDE", "INSIST", "INSULT", "INSURE", "INTAKE", "INTEND", "INTENT", "INVENT", "INVEST", "INVITE", "ISLAND", "ISSUED", "ISSUES", "ITSELF", "JACKET", "JAILED", "JARGON", "JERKED", "JERSEY", "JEWELS", "JOCKEY", "JOINED", "JOINTS", "JOKING", "JUDGED", "JUDGES", "JUMBLE", "JUMPED", "JUMPER", "JUNGLE", "KEENLY", "KEEPER", "KETTLE", "KICKED", "KIDNEY", "KILLED", "KILLER", "KINDLY", "KISSED", "KISSES", "KNIGHT", "KNIVES", "LABELS", "LABOUR", "LACKED", "LADDER", "LADIES", "LANDED", "LARGER", "LARVAE", "LASHES", "LASTED", "LASTLY", "LATELY", "LATEST", "LATTER", "LAUGHS", "LAUNCH", "LAWYER", "LAYERS", "LAYING", "LAYOUT", "LEADER", "LEAGUE", "LEANED", "LEARNS", "LEARNT", "LEASES", "LEAVES", "LEGACY", "LEGEND", "LEGION", "LENDER", "LENGTH", "LENSES", "LESION", "LESSON", "LETTER", "LEVELS", "LICKED", "LIFTED", "LIGHTS", "LIKING", "LIMITS", "LINING", "LINKED", "LIQUID", "LIQUOR", "LISTED", "LISTEN", "LITRES", "LITTER", "LITTLE", "LIVING", "LOADED", "LOCALS", "LOCATE", "LOCKED", "LODGED", "LONGED", "LONGER", "LOOKED", "LOSERS", "LOSING", "LOSSES", "LOUDER", "LOUDLY", "LOUNGE", "LOVERS", "LOVING", "LOWEST", "LUXURY", "LYRICS", "MAGNET", "MAINLY", "MAKERS", "MAKING", "MALICE", "MAMMAL", "MANAGE", "MANNER", "MANTLE", "MANUAL", "MARBLE", "MARGIN", "MARKED", "MARKER", "MARKET", "MARROW", "MASSES", "MASTER", "MATRIX", "MATTER", "MEADOW", "MEDALS", "MEDIUM", "MELODY", "MEMBER", "MEMORY", "MENACE", "MERELY", "MERGER", "MERITS", "METALS", "METHOD", "METRES", "MIDDAY", "MIDDLE", "MILDLY", "MINERS", "MINING", "MINUTE", "MIRROR", "MISERY", "MISSED", "MISSES", "MISUSE", "MIXING", "MOANED", "MODELS", "MODIFY", "MODULE", "MOMENT", "MONIES", "MONKEY", "MONTHS", "MORALE", "MORALS", "MORTAR", "MOSAIC", "MOSQUE", "MOSTLY", "MOTHER", "MOTIFS", "MOTION", "MOTIVE", "MOTORS", "MOUTHS", "MOVIES", "MOVING", "MUCOSA", "MURDER", "MURMUR", "MUSCLE", "MUSEUM", "MUSTER", "MYRIAD", "MYSELF", "NAMELY", "NATION", "NATURE", "NEARBY", "NEARER", "NEARLY", "NEATLY", "NEEDED", "NEEDLE", "NEPHEW", "NERVES", "NEWEST", "NICELY", "NIGHTS", "NINETY", "NOBLES", "NOBODY", "NODDED", "NOISES", "NOTICE", "NOTIFY", "NOTING", "NOTION", "NOUGHT", "NOVELS", "NOVICE", "NUCLEI", "NUMBER", "NURSES", "OBJECT", "OBTAIN", "OCCUPY", "OCCURS", "OCEANS", "OFFERS", "OFFICE", "OFFSET", "OLDEST", "ONIONS", "OPENED", "OPENER", "OPENLY", "OPERAS", "OPPOSE", "OPTING", "OPTION", "ORANGE", "ORDEAL", "ORDERS", "ORGANS", "ORIGIN", "OTHERS", "OUTFIT", "OUTING", "OUTLET", "OUTPUT", "OUTSET", "OWNERS", "OXYGEN", "PACKED", "PACKET", "PALACE", "PANELS", "PAPERS", "PARADE", "PARCEL", "PARDON", "PARENT", "PARISH", "PARITY", "PARKED", "PARROT", "PARTED", "PARTLY", "PASSED", "PASSES", "PASTRY", "PATENT", "PATROL", "PATRON", "PATTED", "PAUSED", "PAVING", "PAYERS", "PAYING", "PEARLS", "PEERED", "PENCIL", "PEOPLE", "PEPPER", "PERIOD", "PERMIT", "PERSON", "PETALS", "PETROL", "PHASES", "PHONED", "PHONES", "PHOTOS", "PHRASE", "PICKED", "PICNIC", "PIECES", "PIGEON", "PILLAR", "PILLOW", "PILOTS", "PIRATE", "PISTOL", "PLACED", "PLACES", "PLAGUE", "PLAINS", "PLANES", "PLANET", "PLANTS", "PLAQUE", "PLASMA", "PLATES", "PLAYED", "PLAYER", "PLEASE", "PLEDGE", "PLENTY", "PLIGHT", "POCKET", "POETRY", "POINTS", "POISON", "POLICE", "POLICY", "POLISH", "POLLEN", "PONIES", "POORER", "POORLY", "POPPED", "PORTAL", "PORTER", "POSING", "POSTED", "POSTER", "POTATO", "POUNDS", "POURED", "POWDER", "POWERS", "PRAISE", "PRAYED", "PRAYER", "PREFER", "PRETTY", "PRICED", "PRICES", "PRIEST", "PRINCE", "PRINTS", "PRISON", "PRIZES", "PROBES", "PROFIT", "PROVED", "PROVES", "PUBLIC", "PULLED", "PULSES", "PUNISH", "PUPILS", "PURELY", "PURITY", "PURSUE", "PUSHED", "PUZZLE", "PYLORI", "QUARRY", "QUOTAS", "QUOTED", "QUOTES", "RABBIT", "RACING", "RACISM", "RACIST", "RACKET", "RADIOS", "RADIUS", "RAISED", "RAISES", "RANGED", "RANGES", "RARELY", "RARITY", "RATHER", "RATING", "RATIOS", "READER", "REALLY", "REASON", "REBELS", "RECALL", "RECIPE", "RECKON", "RECORD", "RECTOR", "REDUCE", "REFERS", "REFLEX", "REFLUX", "REFORM", "REFUGE", "REFUSE", "REGAIN", "REGARD", "REGIME", "REGION", "REGRET", "REJECT", "RELATE", "RELICS", "RELIED", "RELIEF", "RELIES", "REMAIN", "REMARK", "REMEDY", "REMIND", "REMOVE", "RENDER", "RENTAL", "REPAID", "REPAIR", "REPEAT", "REPLAY", "REPORT", "RESCUE", "RESIGN", "RESIST", "RESORT", "RESTED", "RESULT", "RESUME", "RETAIN", "RETINA", "RETIRE", "RETURN", "REVEAL", "REVERT", "REVIEW", "REVISE", "REVIVE", "REVOLT", "REWARD", "RHYTHM", "RIBBON", "RICHER", "RICHLY", "RIDDEN", "RIDERS", "RIDGES", "RIDING", "RIFLES", "RIGHTS", "RIPPED", "RISING", "RITUAL", "RIVALS", "RIVERS", "ROARED", "ROBOTS", "ROCKET", "ROLLED", "ROLLER", "ROMANS", "ROOTED", "ROUNDS", "ROUTES", "RUBBED", "RUBBER", "RUBBLE", "RUINED", "RULERS", "RULING", "RUMOUR", "RUNNER", "RUNWAY", "RUSHED", "SACKED", "SADDLE", "SAFELY", "SAFEST", "SAFETY", "SAILED", "SAILOR", "SAINTS", "SALADS", "SALARY", "SALMON", "SALOON", "SAMPLE", "SAUCER", "SAVING", "SAYING", "SCALES", "SCENES", "SCHEME", "SCHOOL", "SCORED", "SCORER", "SCORES", "SCOUTS", "SCRAPS", "SCREAM", "SCREEN", "SCREWS", "SCRIPT", "SEALED", "SEAMEN", "SEARCH", "SEASON", "SEATED", "SECOND", "SECRET", "SECTOR", "SECURE", "SEEING", "SEEMED", "SEIZED", "SELDOM", "SELECT", "SELLER", "SENATE", "SENSED", "SENSES", "SERIES", "SERMON", "SERVED", "SERVER", "SERVES", "SETTEE", "SETTLE", "SEVENS", "SEWAGE", "SEWING", "SEXISM", "SEXIST", "SHADES", "SHADOW", "SHAFTS", "SHAKEN", "SHAPED", "SHAPES", "SHARED", "SHARES", "SHEETS", "SHELLS", "SHERRY", "SHIELD", "SHIFTS", "SHIRTS", "SHOCKS", "SHORES", "SHORTS", "SHOULD", "SHOUTS", "SHOWED", "SHOWER", "SHRINE", "SHRUBS", "SIGHED", "SIGHTS", "SIGNAL", "SIGNED", "SILVER", "SIMPLY", "SINGER", "SIPPED", "SISTER", "SKETCH", "SKILLS", "SKIRTS", "SLAVES", "SLEEVE", "SLICES", "SLIDES", "SLOGAN", "SLOPES", "SLOWED", "SLOWER", "SLOWLY", "SMELLS", "SMILED", "SMILES", "SMOKED", "SNAKES", "SOCCER", "SOCKET", "SODIUM", "SOFTEN", "SOFTER", "SOFTLY", "SOLELY", "SOLIDS", "SOLVED", "SOONER", "SORROW", "SORTED", "SOUGHT", "SOUNDS", "SOURCE", "SOVIET", "SPACES", "SPARED", "SPEAKS", "SPEECH", "SPEEDS", "SPELLS", "SPENDS", "SPHERE", "SPIDER", "SPINES", "SPIRAL", "SPIRIT", "SPLASH", "SPOKEN", "SPONGE", "SPORTS", "SPOUSE", "SPRANG", "SPREAD", "SPRING", "SQUADS", "SQUARE", "SQUASH", "STABLE", "STAGED", "STAGES", "STAIRS", "STAKES", "STALLS", "STAMPS", "STANCE", "STANDS", "STAPLE", "STARED", "STARTS", "STATED", "STATES", "STATUE", "STATUS", "STAYED", "STENCH", "STEREO", "STICKS", "STITCH", "STOCKS", "STOLEN", "STONES", "STORED", "STORES", "STOREY", "STORMS", "STRAIN", "STRAND", "STRAPS", "STRATA", "STREAK", "STREAM", "STREET", "STRESS", "STRIDE", "STRIKE", "STRING", "STRIPS", "STRODE", "STROKE", "STROLL", "STRUCK", "STUDIO", "STYLES", "SUBMIT", "SUBURB", "SUCKED", "SUFFER", "SUITED", "SUMMED", "SUMMER", "SUMMIT", "SUMMON", "SUNSET", "SUPPER", "SUPPLY", "SURELY", "SURVEY", "SWEETS", "SWITCH", "SWORDS", "SYMBOL", "SYNTAX", "SYSTEM", "TABLES", "TABLET", "TACKLE", "TACTIC", "TAILOR", "TAKING", "TALENT", "TALKED", "TALLER", "TANGLE", "TANKER", "TAPPED", "TARGET", "TARIFF", "TARMAC", "TASTED", "TASTES", "TAUGHT", "TEASED", "TEMPER", "TEMPLE", "TENANT", "TENDED", "TENNIS", "TENURE", "TERMED", "TERROR", "TESTED", "THANKS", "THEIRS", "THEMES", "THEORY", "THESES", "THESIS", "THIGHS", "THINGS", "THINKS", "THINLY", "THIRDS", "THIRTY", "THOUGH", "THREAD", "THREAT", "THRILL", "THROAT", "THRONE", "THROWN", "THROWS", "THRUST", "TICKET", "TIGERS", "TIGHTS", "TILLER", "TIMBER", "TIMING", "TIPPED", "TISSUE", "TITLES", "TOILET", "TOKENS", "TOMATO", "TONGUE", "TONNES", "TOPICS", "TOPPED", "TORIES", "TORQUE", "TOSSED", "TOWARD", "TOWELS", "TOWERS", "TRACED", "TRACES", "TRACKS", "TRACTS", "TRADER", "TRADES", "TRAINS", "TRAITS", "TRAUMA", "TRAVEL", "TREATS", "TREATY", "TRENCH", "TRENDS", "TRIALS", "TRIBES", "TRICKS", "TROOPS", "TROPHY", "TRUCKS", "TRUSTS", "TRUTHS", "TRYING", "TUCKED", "TUGGED", "TUMOUR", "TUNNEL", "TURKEY", "TURNED", "TUTORS", "TWELVE", "TWENTY", "TYPING", "ULCERS", "UNDULY", "UNEASE", "UNIONS", "UNITED", "UNLESS", "UNLIKE", "UNREST", "UPDATE", "UPHELD", "UPLAND", "UPTAKE", "URGING", "VACUUM", "VALLEY", "VALUED", "VALUES", "VALVES", "VANITY", "VAPOUR", "VARIED", "VARIES", "VASTLY", "VELVET", "VENDOR", "VENUES", "VERSES", "VERSUS", "VESSEL", "VICTIM", "VIDEOS", "VIEWED", "VIEWER", "VIGOUR", "VILLAS", "VIOLIN", "VIRTUE", "VISION", "VISITS", "VOICES", "VOLUME", "VOTERS", "VOTING", "VOWELS", "VOYAGE", "WAGONS", "WAITED", "WAITER", "WAKING", "WALKED", "WALLET", "WALNUT", "WANDER", "WANTED", "WARDEN", "WARILY", "WARMER", "WARMLY", "WARMTH", "WARNED", "WASHED", "WASTED", "WASTES", "WATERS", "WAVING", "WEAKEN", "WEAKER", "WEAKLY", "WEALTH", "WEAPON", "WEEKLY", "WEIGHT", "WHALES", "WHEELS", "WHILST", "WHISKY", "WHITES", "WHOLLY", "WICKET", "WIDELY", "WIDEST", "WIDOWS", "WILDLY", "WINDOW", "WINGER", "WINNER", "WINTER", "WIPING", "WIRING", "WISDOM", "WISELY", "WISHED", "WISHES", "WITHIN", "WIZARD", "WOLVES", "WONDER", "WORKED", "WORKER", "WORLDS", "WOUNDS", "WRISTS", "WRITER", "WRITES", "YACHTS", "YELLED", "YIELDS", "YOUTHS" };
    private List<string> grids = new List<string> { "123456..............................", "......123456........................", "............123456..................", "..................123456............", "........................123456......", "..............................123456", "654321..............................", "......654321........................", "............654321..................", "..................654321............", "........................654321......", "..............................654321", "1.....2.....3.....4.....5.....6.....", ".1.....2.....3.....4.....5.....6....", "..1.....2.....3.....4.....5.....6...", "...1.....2.....3.....4.....5.....6..", "....1.....2.....3.....4.....5.....6.", ".....1.....2.....3.....4.....5.....6", "6.....5.....4.....3.....2.....1.....", ".6.....5.....4.....3.....2.....1....", "..6.....5.....4.....3.....2.....1...", "...6.....5.....4.....3.....2.....1..", "....6.....5.....4.....3.....2.....1.", ".....6.....5.....4.....3.....2.....1", "1......2......3......4......5......6", ".....1....2....3....4....5....6.....", "6......5......4......3......2......1", ".....6....5....4....3....2....1....." };
    private string orientations = "˃˃˃˃˃˃˂˂˂˂˂˂˅˅˅˅˅˅˄˄˄˄˄˄┘└┌┐";
    private int chosenOrientation = -1;
    private int chosenWord = -1;
    private string finalGrid = "";
    private string creationGrid = "";
    private string currentChar = "";

    private int X = 0;
    private string acceptableXValues = "nynnnynynnnynynnnynynnnynynnnynynnny";
    private string indicators = "";
    private string serialNumber = "";
    private string initialCipherSequence = "";

    private string keyA = "";
    private string keyB = "";
    private string keyC = "";
    private string keyD = "";
    private string keyE = "";
    private string keyF = "";

    private List<int> triangleNumbers = new List<int> { 28, 29, 21, 30, 22, 15, 31, 23, 16, 10, 32, 24, 17, 11, 6, 33, 25, 18, 12, 7, 3, 34, 26, 19, 13, 8, 4, 1, 35, 27, 20, 14, 9, 5, 2, 0 };
    private List<int> squareNumbers = new List<int> { 30, 24, 18, 12, 6, 0, 31, 25, 19, 13, 7, 1, 32, 26, 20, 14, 8, 2, 33, 27, 21, 15, 9, 3, 34, 28, 22, 16, 10, 4, 35, 29, 23, 17, 11, 5 };
    private string converting = "";
    private string converted = "";
    private string ciphertext = "";

    private List<string> rotatedGrid = new List<string> { "6.....5.....4.....3.....2.....1.....", ".6.....5.....4.....3.....2.....1....", "..6.....5.....4.....3.....2.....1...", "...6.....5.....4.....3.....2.....1..", "....6.....5.....4.....3.....2.....1.", ".....6.....5.....4.....3.....2.....1", "1.....2.....3.....4.....5.....6.....", ".1.....2.....3.....4.....5.....6....", "..1.....2.....3.....4.....5.....6...", "...1.....2.....3.....4.....5.....6..", "....1.....2.....3.....4.....5.....6.", ".....1.....2.....3.....4.....5.....6", "..............................123456", "........................123456......", "..................123456............", "............123456..................", "......123456........................", "123456..............................", "..............................654321", "........................654321......", "..................654321............", "............654321..................", "......654321........................", "654321..............................", ".....6....5....4....3....2....1.....", "1......2......3......4......5......6", ".....1....2....3....4....5....6.....", "6......5......4......3......2......1" };
    private string groups = "112233112233445566445566778899778899";
    private string arrows = "┌˄┐˂˃└˅┘";
    private List<string> stringFormats = new List<string> { "DnnnDnnnD", "nnDnnDnnD", "DnnDnnDnn", "nnnDnnnDD", "DDnnnDnnn", "nnDnDnDnn", "nnnDDDnnn", "nDnnDnnDn" };
    private string answerString = "";
    private string answerCode = "";
    private int numberOfInputs = 0;
    private int correctDirection = 0;
    private int numberOfDirections = 0;
    private int numberOfDigits = 0;
    private List<string> loggingColors = new List<string> { "red, so press up.", "green, so press left.", "white, so press down.", "blue, so press right." };
    private bool inSubmissionMode = false;
    private List<string> directionNames = new List<string> { "up", "left", "down", "right" };
    private List<string> soundNames = new List<string> { "Red", "Green", "White", "Blue" };
    private string answerFormat;
	
	#pragma warning disable 0649
    private bool TwitchPlaysActive; 
    private IDictionary<string, object> tpAPI;
    #pragma warning restore 0649
    float waitTime = 0f;

    private OutrageousSettings Settings = new OutrageousSettings();
    bool resetsOnStrike = true;

    void Awake () {
        moduleId = moduleIdCounter++;

        ModConfig<OutrageousSettings> modConfig = new ModConfig<OutrageousSettings>("OutrageousSettings");
        //Read from the settings file, or create one if one doesn't exist
        Settings = modConfig.Settings;
        //Update the settings file incase there was an error during read
        modConfig.Settings = Settings;

        resetsOnStrike = Settings.Resets;
        Debug.LogFormat("<Outrageous #{0}> Resets on strike: {1}", moduleId, resetsOnStrike ? "Enabled" : "Disabled");

        StartButton.OnInteract += delegate () { PressStart(); return false; };

        foreach (KMSelectable Number in Numbers) {
            Number.OnInteract += delegate () { NumberPress(Number); return false; };
        }

        foreach (KMSelectable Direction in Directions) {
            Direction.OnInteract += delegate () { DirectionPress(Direction); return false; };
        }

        foreach (KMSelectable Symbol in Symbols) {
            Symbol.OnHighlight += delegate { Hover(Symbol); return; };
            Symbol.OnHighlightEnded += delegate { HoverEnded(); return; };
        }
		
		 GetComponent<KMBombModule>().OnActivate += OutrageousInTP;
    }
	
	void OutrageousInTP()
	{
        if (TwitchPlaysActive)
        {
            waitTime = 9.75f;
            GameObject tpAPIGameObject = GameObject.Find("TwitchPlays_Info");
            //To make the module can be tested in test harness, check if the gameObject exists.
            if (tpAPIGameObject != null)
                tpAPI = tpAPIGameObject.GetComponent<IDictionary<string, object>>();
            else
                TwitchPlaysActive = false;
        }
	}

    // Use this for initialization
    void Start () {
        SubmissionMode.SetActive(false);
        GeneratePuzzle();
    }

    void GeneratePuzzle () {
        ciphertext = "";

        chosenOrientation = UnityEngine.Random.Range(0, grids.Count());
        chosenWord = UnityEngine.Random.Range(0, words.Count());
        finalGrid = grids[chosenOrientation];
        finalGrid = finalGrid.Replace("1".ToString(), words[chosenWord][0].ToString()).Replace("2".ToString(), words[chosenWord][1].ToString()).Replace("3".ToString(), words[chosenWord][2].ToString()).Replace("4".ToString(), words[chosenWord][3].ToString()).Replace("5".ToString(), words[chosenWord][4].ToString()).Replace("6".ToString(), words[chosenWord][5].ToString());
        Debug.LogFormat("[Outrageous #{0}] Generated word in 6x6 grid: {1}", moduleId, finalGrid);
        creationGrid = "";
		for (int i = 0; i < 36; i++) {
            if (finalGrid[i].ToString() == ".".ToString()) {
                currentChar = base36.PickRandom().ToString();
            } else {
                currentChar = finalGrid[i].ToString();
            }
            creationGrid = creationGrid + currentChar;
        }
        finalGrid = creationGrid;
        Debug.LogFormat("[Outrageous #{0}] Filled in 6x6 grid: {1}", moduleId, finalGrid);

        indicators = Bomb.GetIndicators().Join("");
        X = 0;
        for (int j = 0; j < indicators.Length; j++) {
            X = (X + (base36.IndexOf(indicators[j]) - 9)) % 36;
        }
        Debug.LogFormat("[Outrageous #{0}] The X value starts at {1}.", moduleId, X);
        while (acceptableXValues[X].ToString() == "n".ToString()) {
            X = (X + 1) % 36;
        }
        Debug.LogFormat("[Outrageous #{0}] Modifying the X value gives us {1}.", moduleId, X);

        initialCipherSequence = "";
        serialNumber = Bomb.GetSerialNumber();
        for (int k = 0; k < 36; k++) {
            currentChar = serialNumber[k % 6].ToString();
            while (initialCipherSequence.IndexOf(currentChar) != -1) {
                currentChar = base36[(base36.IndexOf(currentChar) + X) % 36].ToString();
            }
            initialCipherSequence = initialCipherSequence + currentChar;
        }
        Debug.LogFormat("[Outrageous #{0}] The initial cipher sequence is: {1}", moduleId, initialCipherSequence);

        keyA = finalGrid.Substring(0, 6);
        keyB = finalGrid.Substring(6, 6);
        keyC = finalGrid.Substring(12, 6);
        keyD = finalGrid.Substring(18, 6);
        keyE = finalGrid.Substring(24, 6);
        keyF = finalGrid.Substring(30, 6);

        Debug.LogFormat("[Outrageous #{0}] Six keys: {1}, {2}, {3}, {4}, {5}, {6}", moduleId, keyA, keyB, keyC, keyD, keyE, keyF);
        for (int l = 0; l < 6; l++) {
            switch (l) {
                case 0: converting = initialCipherSequence; break;
                case 1: converting = keyA; break;
                case 2: converting = keyB; break;
                case 3: converting = keyC; break;
                case 4: converting = keyD; break;
                case 5: converting = keyE; break;
                default: Debug.Log("At NASA this is what we like to call a critical oversight."); break;
            }
            if (base36.IndexOf(serialNumber[l]) > 9) {
                //letter
                for (int m = 0; m < 36; m++) {
                    converted = converted + converting[triangleNumbers[m]].ToString();
                }
            } else {
                //number
                for (int n = 0; n < 36; n++) {
                    converted = converted + converting[squareNumbers[n]].ToString();
                }
            }
            switch (l) {
                case 0: keyA = converted; Debug.LogFormat("[Outrageous #{0}] 1st Cipher Key: {1}", moduleId, keyA); break;
                case 1: keyB = converted; Debug.LogFormat("[Outrageous #{0}] 2nd Cipher Key: {1}", moduleId, keyB); break;
                case 2: keyC = converted; Debug.LogFormat("[Outrageous #{0}] 3rd Cipher Key: {1}", moduleId, keyC); break;
                case 3: keyD = converted; Debug.LogFormat("[Outrageous #{0}] 4th Cipher Key: {1}", moduleId, keyD); break;
                case 4: keyE = converted; Debug.LogFormat("[Outrageous #{0}] 5th Cipher Key: {1}", moduleId, keyE); break;
                case 5: keyF = converted; Debug.LogFormat("[Outrageous #{0}] 6th Cipher Key: {1}", moduleId, keyF); break;
                default: Debug.Log("At NASA this is what we like to call a critical oversight."); break;
            }
            converted = "";
        }

        for (int p = 0; p < 36; p++) {
            if (p < 6) { ciphertext = ciphertext + base36[keyA.IndexOf(finalGrid[p])].ToString(); }
            else if (p < 12) { ciphertext = ciphertext + base36[keyB.IndexOf(finalGrid[p])].ToString(); }
            else if (p < 18) { ciphertext = ciphertext + base36[keyC.IndexOf(finalGrid[p])].ToString(); }
            else if (p < 24) { ciphertext = ciphertext + base36[keyD.IndexOf(finalGrid[p])].ToString(); }
            else if (p < 30) { ciphertext = ciphertext + base36[keyE.IndexOf(finalGrid[p])].ToString(); }
            else { ciphertext = ciphertext + base36[keyF.IndexOf(finalGrid[p])].ToString(); }
        }
        Debug.LogFormat("[Outrageous #{0}] Complete ciphertext: {1}", moduleId, ciphertext);

        Text.text = ciphertext.Substring(0, 6) + "\n" + ciphertext.Substring(6, 6) + "\n" + ciphertext.Substring(12, 6) + "\n" + ciphertext.Substring(18, 6) + "\n" + ciphertext.Substring(24, 6) + "\n" + ciphertext.Substring(30, 6);

        answerString = finalGrid[rotatedGrid[chosenOrientation].IndexOf("1")].ToString() + finalGrid[rotatedGrid[chosenOrientation].IndexOf("2")].ToString() + finalGrid[rotatedGrid[chosenOrientation].IndexOf("3")].ToString() + finalGrid[rotatedGrid[chosenOrientation].IndexOf("4")].ToString() + finalGrid[rotatedGrid[chosenOrientation].IndexOf("5")].ToString() + finalGrid[rotatedGrid[chosenOrientation].IndexOf("6")].ToString();

        Debug.LogFormat("[Outrageous #{0}] The input word: {1}", moduleId, answerString);

        answerCode = groups[base36.IndexOf(answerString[0])].ToString() + groups[base36.IndexOf(answerString[1])].ToString() + groups[base36.IndexOf(answerString[2])].ToString() + groups[base36.IndexOf(answerString[3])].ToString() + groups[base36.IndexOf(answerString[4])].ToString() + groups[base36.IndexOf(answerString[5])].ToString();

        Debug.LogFormat("[Outrageous #{0}] The input numbers: {1}", moduleId, answerCode);

        answerFormat = stringFormats[arrows.IndexOf(orientations[chosenOrientation])];
        Debug.LogFormat("[Outrageous #{0}] Format on input sequence: {1}", moduleId, answerFormat);
    }

    void Hover (KMSelectable Symbol) {
        for (int a = 0; a < 36; a++) {
            if (Symbol == Symbols[a]) {
                Text.text = "";
                SingleCharacter.text = ciphertext[a].ToString();
            }
        }
    }

    void HoverEnded () {
        Text.text = ciphertext.Substring(0, 6) + "\n" + ciphertext.Substring(6, 6) + "\n" + ciphertext.Substring(12, 6) + "\n" + ciphertext.Substring(18, 6) + "\n" + ciphertext.Substring(24, 6) + "\n" + ciphertext.Substring(30, 6);
        SingleCharacter.text = "";
    }

    void PressStart() {
        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
        inSubmissionMode = true;
        DefaultMode.SetActive(false);
        SubmissionMode.SetActive(true);
        StartCoroutine(Submission());
    }

    void GiveStrike() {
        StopCoroutine(Submission());
        numberOfInputs = 0;
        numberOfDigits = 0;
        numberOfDirections = 0;
        GetComponent<KMBombModule>().HandleStrike();
        inSubmissionMode = false;
        DefaultMode.SetActive(true);
        SubmissionMode.SetActive(false);
        if (resetsOnStrike) {
            GeneratePuzzle();
        }
    }

    private IEnumerator Submission() {
        if (inSubmissionMode) {
            yield return new WaitForSeconds(5.0f); //1st flash
            if (inSubmissionMode) {
                correctDirection = UnityEngine.Random.Range(0, 4) + 1;
                LEDObject.GetComponent<MeshRenderer>().material = LEDColors[correctDirection];
                if (TwitchPlaysActive)
                    tpAPI["ircConnectionSendMessage"] = "The LED flashed " + soundNames[correctDirection - 1] + "!";
                Debug.LogFormat("[Outrageous #{0}] The color for the 1st flash is {1}", moduleId, loggingColors[correctDirection - 1]);
                Audio.PlaySoundAtTransform(soundNames[correctDirection-1], transform);
                yield return new WaitForSeconds(0.25f);
                LEDObject.GetComponent<MeshRenderer>().material = LEDColors[0];
                yield return new WaitForSeconds(waitTime != 0f ? waitTime : 4.75f); //2nd flash
                if (inSubmissionMode) {
                    if (numberOfDirections != 1) {
                        Debug.LogFormat("[Outrageous #{0}] You didn't press the 1st direction in time, strike! Generating new puzzle...", moduleId);
                        GiveStrike();
                    } else {
                        correctDirection = UnityEngine.Random.Range(0, 4) + 1;
                        LEDObject.GetComponent<MeshRenderer>().material = LEDColors[correctDirection];
                        if (TwitchPlaysActive)
                            tpAPI["ircConnectionSendMessage"] = "The LED flashed " + soundNames[correctDirection - 1] + "!";
                        Debug.LogFormat("[Outrageous #{0}] The color for the 2nd flash is {1}", moduleId, loggingColors[correctDirection - 1]);
                        Audio.PlaySoundAtTransform(soundNames[correctDirection-1], transform);
                    }
                    yield return new WaitForSeconds(0.25f);
                    LEDObject.GetComponent<MeshRenderer>().material = LEDColors[0];
                    yield return new WaitForSeconds(waitTime != 0f ? waitTime : 4.75f); //3rd flash
                    if (inSubmissionMode) {
                        if (numberOfDirections != 2) {
                            Debug.LogFormat("[Outrageous #{0}] You didn't press the 2nd direction in time, strike! Generating new puzzle...", moduleId);
                            GiveStrike();
                        } else {
                            correctDirection = UnityEngine.Random.Range(0, 4) + 1;
                            LEDObject.GetComponent<MeshRenderer>().material = LEDColors[correctDirection];
                            if (TwitchPlaysActive)
                                tpAPI["ircConnectionSendMessage"] = "The LED flashed " + soundNames[correctDirection - 1] + "!";
                            Debug.LogFormat("[Outrageous #{0}] The color for the 3rd flash is {1}", moduleId, loggingColors[correctDirection - 1]);
                            Audio.PlaySoundAtTransform(soundNames[correctDirection-1], transform);
                        }
                        yield return new WaitForSeconds(0.25f);
                        LEDObject.GetComponent<MeshRenderer>().material = LEDColors[0];
                        yield return new WaitForSeconds(waitTime != 0f ? waitTime : 4.75f);
                        if (inSubmissionMode) {
                            Debug.LogFormat("[Outrageous #{0}] You didn't press the 3rd direction in time, strike! Generating new puzzle...", moduleId);
                            GiveStrike();
                        }
                    }
                }
            }
        }
    }

    void NumberPress(KMSelectable Number) {
        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
        for (int i = 0; i < 9; i++) {
            if (Number == Numbers[i]) {
                if (answerCode[numberOfDigits].ToString() == base36[i + 1].ToString() && answerFormat[numberOfInputs] == 'n') {
                    Debug.LogFormat("[Outrageous #{0}] You pressed {1}, that is correct.", moduleId, i+1);
                    numberOfDigits++;
                    numberOfInputs++;
                    if (numberOfInputs == 9) {
                        GetComponent<KMBombModule>().HandlePass();
                        SubmissionMode.SetActive(false);
                        LEDObject.SetActive(false);
                        Debug.LogFormat("[Outrageous #{0}] Submitted all correct inputs, module solved.", moduleId);
                        inSubmissionMode = false;
                        Audio.PlaySoundAtTransform("GG", transform);
                    }
                } else {
                    Debug.LogFormat("[Outrageous #{0}] You pressed wrong number ({1}), strike! Generating new puzzle...", moduleId, i+1);
                    GiveStrike();
                }
            }
        }
    }

    void DirectionPress(KMSelectable Direction) {
        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
        for (int i = 0; i < 4; i++) {
            if (Direction == Directions[i]) {
                if (i == correctDirection - 1 && answerFormat[numberOfInputs] == 'D') {
                    Debug.LogFormat("[Outrageous #{0}] You pressed {1}, that is correct.", moduleId, directionNames[i]);
                    correctDirection = 0;
                    numberOfDirections = numberOfDirections + 1;
                    numberOfInputs = numberOfInputs + 1;
                    if (numberOfInputs == 9) {
                        GetComponent<KMBombModule>().HandlePass();
                        SubmissionMode.SetActive(false);
                        LEDObject.SetActive(false);
                        Debug.LogFormat("[Outrageous #{0}] Submitted all correct inputs, module solved.", moduleId);
                        inSubmissionMode = false;
                        Audio.PlaySoundAtTransform("GG", transform);
                    }
                } else {
                    Debug.LogFormat("[Outrageous #{0}] You pressed the wrong direction ({1}), strike! Generating new puzzle...", moduleId, directionNames[i]);
                    GiveStrike();
                }
            }
        }
    }
	
	//twitch plays
    #pragma warning disable 414
    private readonly string TwitchHelpMessage = @"Use <!{0} highlight A1 B2 C3 D4> to hover over the creatures in those positions. Use <!{0} start> to press the start button. Use <!{0} press 123UDR456> to press those buttons. LED changes will be announced in chat.";
    #pragma warning restore 414
	IEnumerator Press(KMSelectable btn, float delay)
    {
        btn.OnInteract();
        yield return new WaitForSeconds(delay);
    }

	IEnumerator ProcessTwitchCommand(string command)
    {
        command = command.Trim().ToUpperInvariant();
        Match mCyc = Regex.Match(command, @"^(?:CYCLE|HIGHLIGHT)((?:\s+[A-F][1-6])+)$");
        Match mSub = Regex.Match(command, @"^(?:PRESS|SUBMIT)((?:\s*[1-9ULDR])+)$");
        if (mCyc.Success && DefaultMode.activeSelf)
        {
            yield return null;
            foreach (string coord in mCyc.Groups[1].Value.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
            {
                Debug.Log(mCyc.Groups[1].Value.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Join());
                int pos = 6 * (coord[1] - '1') + (coord[0] - 'A');
                Symbols[pos].OnHighlight();
                yield return "trycancel";
                yield return new WaitForSeconds(1.5f);
                Symbols[pos].OnHighlightEnded();
            }
        }
        else if (command == "START" && DefaultMode.activeSelf)
        {
            yield return null;
            yield return Press(StartButton, 0.1f);
        }
        else if (mSub.Success && SubmissionMode.activeSelf)
        {
            yield return null;
            foreach (char btn in mSub.Groups[1].Value.Where(x => !char.IsWhiteSpace(x)))
            {
                if ("ULDR".Contains(btn))
                    yield return Press(Directions["ULDR".IndexOf(btn)], 0.1f);
                else yield return Press(Numbers[btn - '1'], 0.1f);
            }
        }
	}

    IEnumerator TwitchHandleForcedSolve()
    {
        if (DefaultMode.activeSelf)
            yield return Press(StartButton, 0.1f);
        for (int i = numberOfInputs; i < 9; i++)
        {
            if (answerFormat[i] == 'n')
                yield return Press(Numbers[answerCode[numberOfDigits] - '1'], 0.1f);
            else
            {
                yield return new WaitWhile(() => LEDObject.GetComponent<MeshRenderer>().material.name.StartsWith("black"));
                yield return new WaitForSeconds(0.1f);
                yield return Press(Directions[correctDirection - 1], 0.1f);
                yield return new WaitUntil(() => LEDObject.GetComponent<MeshRenderer>().material.name.StartsWith("black"));
            }
        }
    }

    class OutrageousSettings
    {
        public bool Resets = true;
    }

    static Dictionary<string, object>[] TweaksEditorSettings = new Dictionary<string, object>[]
    {
        new Dictionary<string, object>
        {
            { "Filename", "OutrageousSettings.json" },
            { "Name", "Outrageous Settings" },
            { "Listing", new List<Dictionary<string, object>>{
                new Dictionary<string, object>
                {
                    { "Key", "Resets" },
                    { "Text", "Whether or not the module resets on a strike." }
                }
            } }
        }
    };
}
