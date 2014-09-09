using System;

namespace snake
{
    /// <summary>
    /// Allows to set the IP addresses for the user and his adv to receive and send packets
    /// </summary>
    class SetParameters
    {
        public String AddressIPReceiver;
        public String AddressIPSender;

        /// <summary>
        /// Default constructor of the class SetParameters
        /// </summary>
        public SetParameters()
        {
            AddressIPReceiver = "239.1.1.50";
            AddressIPSender = "239.1.1.51";
        }
    }
}
