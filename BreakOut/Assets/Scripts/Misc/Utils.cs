public static class Utils {
    // Convert an Int to a String with no Garbage
    public static void IntToString(int value, char[] buffer) {
        int length = 0;
        int lengthCalc = value;

        // If zero, just return "0"
        if (value == 0) {
            buffer[0] = '0';
            buffer[1] = '\0';
            return;
        }

        // See how long the integer is
        while (lengthCalc > 0) {
            lengthCalc /= 10;
            length++;
        }

        // Terminate the string with a null
        buffer[length] = '\0';

        // Put each digit into the buffer. In reverse, so start at right and work left.
        while (value != 0) {
            buffer[--length] = (char)('0' + value % 10);
            value /= 10;
        }
    }
}
