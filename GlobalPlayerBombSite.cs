using System;


namespace CS_GO_Analysis {

    /// <summary>
    /// This class is a globalisation of the PlayerBombSite
    /// It will be used to determine the global Player SetUp 
    /// for one or multiple Game. 
    /// </summary>
    public class GlobalPlayerBombSite {

        public string Name;

        public int ASite;

        public int BSite; 

        public GlobalPlayerBombSite() {

        }

        public void APosition() {
            ASite++; 
        }

        public void BPosition() {
            BSite++;
        }

        /// <summary>
        /// Get the global site of the player. 
        /// TODO : Find a way to determine surely that the player is from this bombsite
        /// and that the two setups or not almost equals. 
        /// </summary>
        /// <returns></returns>
        public SetUp.BombSite GetGlobalSite() {
            if (ASite > BSite) {
                return SetUp.BombSite.A; 
            } else {
                return SetUp.BombSite.B; 
            }
        }

    }
}
