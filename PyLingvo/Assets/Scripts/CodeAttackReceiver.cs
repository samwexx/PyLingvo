using UnityEngine;

public class CodeAttackReceiver : MonoBehaviour
{
    [Header("Настройки урона")]
    public int strongDamage = 25; // урон при правильном коде
    public int weakDamage = 5;    // урон при ошибке

    public void TriggerCodeAttack()
    {
        // Открываем терминал и говорим, какую функцию вызвать после нажатия кнопки
        TerminalController.instance.Open(CheckAttack,
            "Чтобы нанести сильный удар по волку, напиши: print('hit')");
    }

    private void CheckAttack(string code)
    {
        string clean = code.Replace(" ", "").Replace("\n", "");

        if (clean == "print('hit')" || clean == "print(\"hit\")")
        {
            ApplyDamage(strongDamage);
            TerminalController.instance.outputText.text = "Критический удар!";
        }
        else
        {
            ApplyDamage(weakDamage);
            TerminalController.instance.outputText.text = "Слабый удар...";
        }
    }

    // Универсальный способ нанести урон: ищем метод TakeDamage на этом объекте
    private void ApplyDamage(int damage)
    {
        // Если на волке есть скрипт с методом void TakeDamage(int),
        // он будет вызван. Если нет — ошибки не будет (DontRequireReceiver).
        gameObject.SendMessage("TakeDamage", damage, SendMessageOptions.DontRequireReceiver);
    }
}
