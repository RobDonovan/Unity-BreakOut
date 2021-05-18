public class Consts {
    // Keep all litterals in one place. Makes spell checking and consitancy better if it's all in one place.
    // Also can help with Localization if needed in the future.

    public static class TAGS {
        public const string BOUNDARY_BOUNCE = "BoundaryBounce";
        public const string BOUNDARY_KILL = "BoundaryKill";
        public const string BAT = "Bat";
        public const string BALL = "Ball";
        public const string BAT_ROCKET = "BatRocket";
    }

    public static class LAYERS {
        public const string BALL = "Ball";
        public const string BRICKS = "Brick";
        public const string BRICKS_PHYSICS = "BrickPhysics";
    }

    public static class UI {
        public static class PANEL_AYS {
            public static class APP_EXIT {
                public const string AYS_QUESTION = "Are you Sure?";
                public const string DO_BUTTON = "Exit";
                public const string CANCEL_BUTTON = "Cancel";
            }

            public static class GAME_EXIT {
                public const string AYS_QUESTION = "Quit Game?";
                public const string DO_BUTTON = "Quit";
                public const string CANCEL_BUTTON = "Cancel";
            }
        }
    }

    public const string INPUT_AXIS = "Horizontal";
    public const float GAME_START_DELAY = 2.0f;

    public const int COLLECTABLE_POOL_START_NUM = 5;
    public const int BALL_POOL_START_NUM = 3;

    public static class EDITOR {
        public static class PROPERTIES {
            public const string SCRIPT = "m_Script";
            public const string BRICK_TYPE = "BrickType";
            public const string COLLECTABLE_TYPE = "BrickCollectable";
        }

        public static class LABELS {
            public const string BRICK_TYPE = "Brick Type";
            public const string COLLECTABLE_TYPE = "Brick Collectable";
        }
    }

    public static class SCRIPTABLE_OBJECTS {
        public static class FILE_NAMES {
            public const string WALLS = "Walls";
            public const string WALL = "Wall";
            public const string BRICK = "Brick";
            public const string COLLECTABLE_POWER_BALL = "Collectable_PowerBall";
            public const string COLLECTABLE_TRIPLE_BALL = "Collectable_TripleBall";
            public const string COLLECTABLE_BAT_ROCKET = "Collectable_BatRocket";
        }

        public static class MENU_TEXT {
            private const string BREAK_OUT = "BreakOut";
            private const string COLLECTABLE = "Collectable";

            public const string WALLS = BREAK_OUT + "/" + "Walls";
            public const string WALL = BREAK_OUT + "/" + "Wall";
            public const string BRICK = BREAK_OUT +"/" + "Brick";
            public const string COLLECTABLE_POWER_BALL = BREAK_OUT + "/" + COLLECTABLE + "/" + "Power Ball";
            public const string COLLECTABLE_TRIPLE_BALL = BREAK_OUT + "/" + COLLECTABLE + "/" + "Triple Ball";
            public const string COLLECTABLE_BAT_ROCKET = BREAK_OUT + "/" + COLLECTABLE + "/" + "Bat Rocket";
        }

        public static class MENU_ORDER {
            public const int WALLS = 1;
            public const int WALL = 2;
            public const int BRICK = 3;
            public const int COLLECTABLE_POWER_BALL = 4;
            public const int COLLECTABLE_TRIPLE_BALL = 5;
            public const int COLLECTABLE_BAT_ROCKET = 6;
        }
    }
}
