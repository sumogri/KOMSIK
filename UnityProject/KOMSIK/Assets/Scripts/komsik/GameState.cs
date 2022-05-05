using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace KOMSIK
{
    public class GameState 
    {
        public int CustomPoint => customPoint.Value;
        public int NowTurn => nowTurn.Value;
        public int MaxTurn => maxTurn.Value;
        public Section NowSection => nowSection.Value;
        public GamePhase NowGamePhase => nowGamePhase.Value;
        public IObservable<GamePhase> OnChangeGamePhase => nowGamePhase;

        private ReactiveProperty<int> customPoint = new ReactiveProperty<int>();
        private ReactiveProperty<int> nowTurn = new ReactiveProperty<int>();
        private ReactiveProperty<int> maxTurn = new ReactiveProperty<int>();
        private ReactiveProperty<Section> nowSection = new ReactiveProperty<Section>();
        private ReactiveProperty<GamePhase> nowGamePhase = new ReactiveProperty<GamePhase>();
        private ReactiveProperty<int> phaseIterateTime = new ReactiveProperty<int>(); // �o�g���J��Ԃ���.3��.

        /// <summary>
        /// ������.
        /// </summary>
        public void Init(int maxTurn,int customPoint)
        {
            this.maxTurn.Value = maxTurn;
            this.customPoint.Value = customPoint;
            this.nowSection.Value = Section.Title;
            this.nowGamePhase.Value = GamePhase.None;
            this.phaseIterateTime.Value = 0;
        }

        public void ChangeSection(Section changeTo)
        {
            nowSection.Value = changeTo;
        }

        public void ChangeGamePhase(GamePhase changeTo)
        {
            nowGamePhase.Value = changeTo;
        }

        public enum Section
        {
            Title,
            Game,
            End,
        }

        public void OnTurnEnd()
        {
            nowTurn.Value++;
            customPoint.Value += 3; //�Ă��Ƃ�.1�^�[�����Ƃ̃J�X�^���|�C���g����.
        }

        public enum GamePhase
        {
            None,
            PreTalk,
            Battle,
            Custom,
            AftTalkBad,
            AftTalkGood,
        }
    }
}
