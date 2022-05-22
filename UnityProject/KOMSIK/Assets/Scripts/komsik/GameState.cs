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
        public IObservable<int> OnCustomPointChange => customPoint;
        public IObservable<Section> OnChangeGameSection => nowSection;
        public int PhaseIterateTime => phaseIterateTime.Value;
        public IObservable<int> OnNowTurnValueChange => nowTurn;
        public int LastTurn { get { return MaxTurn - NowTurn; } }

        private ReactiveProperty<int> customPoint = new ReactiveProperty<int>();
        private ReactiveProperty<int> nowTurn = new ReactiveProperty<int>();
        private ReactiveProperty<int> maxTurn = new ReactiveProperty<int>();
        private ReactiveProperty<Section> nowSection = new ReactiveProperty<Section>();
        private ReactiveProperty<GamePhase> nowGamePhase = new ReactiveProperty<GamePhase>();
        private ReactiveProperty<int> phaseIterateTime = new ReactiveProperty<int>(); // バトル繰り返し回数.3回.

        /// <summary>
        /// 初期化.
        /// </summary>
        public void Init(int maxTurn,int customPoint)
        {
            this.maxTurn.Value = maxTurn;
            this.customPoint.Value = customPoint;
            this.nowSection.Value = Section.PreMovie; 
            this.nowGamePhase.Value = GamePhase.None;
            this.phaseIterateTime.Value = 0;
            this.nowTurn.Value = 0;
        }

        public void InitOnBattle(int maxTurn, int customPoin)
        {
            this.maxTurn.Value = 10;
            this.customPoint.Value = 3;
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
            PreMovie,
            Title,
            Game,
            BadEnd,
            GoodEnd,
            NormalEnd,
        }

        public void AddCustomPoint(int adder)
        {
            customPoint.Value += adder;
        }

        public void OnTurnEnd()
        {
            nowTurn.Value++;
            customPoint.Value += 3; //てきとう.1ターンごとのカスタムポイント増加.
        }

        public void OnPhaseEnd()
        {
            phaseIterateTime.Value++;
        }

        public void PaidCustomPoint(int paidPoint)
        {
            customPoint.Value = Mathf.Max(0,CustomPoint-paidPoint);
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
