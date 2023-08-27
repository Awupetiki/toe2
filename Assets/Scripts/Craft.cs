using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Craft : MonoBehaviour {
    private Cursor _curs;
    private GameObject tmp;

    void Start() {
        _curs = GameObject.Find("Cursor").GetComponent<Cursor>();
    }

    void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.name == "OakPlanks" && gameObject.name == "OakPlanks" && Input.GetKey("left shift") && Physics2D.Raycast(transform.position, transform.forward) == GetComponent<BoxCollider2D>()) {
            // сохраняем скрафченный объект в переменную
            tmp = Instantiate(_curs.crafts[0]);

            // назначаем ему позицию
            tmp.transform.position = transform.position; 

            // убираем ему родителя шоб не удалился
            tmp.transform.parent = null;

            // удаляем ресурсы ушедшие на крафт
            Destroy(other.gameObject);
            Destroy(gameObject);

            // и получаем ультра мега говнокод :D
        }
    }
}
