using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KOMSIK
{
    public class CharacterOrigin
    {
        public string Name { get; set; }
        public int HP { get; set; }
        public int CustomDeckID { get; set; }
        public Power Power { get; set; }

        public static CharacterOrigin[] CharacterOrigins { get; } = new CharacterOrigin[5];

        static CharacterOrigin()
        {
            CharacterOrigins[0] = new CharacterOrigin("魔王",1000,0,Power.Enemy);
            CharacterOrigins[1] = new CharacterOrigin("グラン", 100, 1, Power.Own);
            CharacterOrigins[2] = new CharacterOrigin("スカイ", 100, 2, Power.Own);
            CharacterOrigins[3] = new CharacterOrigin("シィ", 100, 3, Power.Own);
            CharacterOrigins[4] = new CharacterOrigin("ユーシャ", 100, 4, Power.Own);
        }

        public static CharacterOrigin GetOrigin(CharacterID id)
        {
            return CharacterOrigins[(int)id];
        }

        public enum CharacterID
        {
            Maou = 0,
            Gran = 1,
            Sky = 2,
            Sea = 3,
            Yusy = 4
        }

        public CharacterOrigin(string name,int hp, int customDeck, Power power)
        {
            Name = name;
            HP = hp;
            CustomDeckID = customDeck;
            Power = power;
        }
    }
}
