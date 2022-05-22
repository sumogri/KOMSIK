using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace KOMSIK
{
    public class CutinView : MonoBehaviour
    {
        [SerializeField] private WordSlotView wordSlotView;
        [SerializeField] private WordReciever wordReciever;
        [SerializeField] private Button cutinBeginButton;
        [SerializeField] private Power power = Power.Own;

        private bool detailActivate = true;
        private bool isCanCutin = true; // �J�b�g�C�������c���Ă��邩
        private bool isSlotined = false; // �������烏�[�h���X���b�g�ɓ����Ă��邩

        // Start is called before the first frame update
        void Start()
        {
            DoSubscribe(GameManager.GameSystem);
        }

        public void WordDetailActiveTurn()
        {
            detailActivate = !detailActivate;
            wordSlotView.SetDetailActivate(detailActivate);
        }

        private void DoSubscribe(GameSystem gameSystem)
        {
            var power = gameSystem.GetMinePower(this.power);
            wordSlotView.DoSubscribe(power.CutinWordState);

            cutinBeginButton.onClick
                .AddListener(() => OnCutinButtonDown(gameSystem));
            power.CharacterState.OnChangeCustomDeckID
                .Subscribe(_ => OnCharacterChenge())
                .AddTo(gameObject);
            power.CutinWordState
                .OnSetFromOrigin.Subscribe(_ => OnSlotInWord())
                .AddTo(gameObject);

            gameSystem.OnWordDoEffect
                .Subscribe(OnWordDoEffect)
                .AddTo(gameObject);

            gameSystem.GameState.OnChangeGamePhase
                .Where(x => x == GameState.GamePhase.Custom)
                .Subscribe(_ => OnCustomStart())
                .AddTo(gameObject);
            gameSystem.GameState.OnChangeGamePhase
                .Where(x => x == GameState.GamePhase.Battle)
                .Subscribe(_ => OnBattleStart())
                .AddTo(gameObject);
        }

        private void OnCustomStart()
        {
            cutinBeginButton.interactable = false;
        }

        private void OnBattleStart()
        {
            if (isCanCutin && isSlotined)
            {
                cutinBeginButton.interactable = true;
            }
        }

        private void OnCharacterChenge()
        {
            wordReciever.SetAcseptActive(true);
            isCanCutin = true;
            wordSlotView.Model.Init();
            isSlotined = false;
        }

        private void OnWordDoEffect(WordState word)
        {
            wordSlotView.SetHighilightActivate2(wordSlotView.Model == word);
        }

        private void OnCutinButtonDown(GameSystem gameSystem) 
        {
            gameSystem.BattleCutinWord(wordSlotView.Model);
            wordSlotView.SetHighilightActivate(true);
            Disactivate();
            isCanCutin = false; //���������J�b�g�C�������Ȃ�.
        }

        private void OnSlotInWord()
        {
            isSlotined = true;
        }

        private void Disactivate()
        {
            cutinBeginButton.interactable = false;
            wordReciever.SetAcseptActive(false);
        }
    }
}
