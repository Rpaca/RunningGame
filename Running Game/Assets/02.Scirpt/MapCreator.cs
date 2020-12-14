using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block
{
    // 블록의 종류를 나타내는 열거체.
    public enum TYPE
    {
        NONE = -1, // 없음.
        FLOOR = 0, // 마루.
        HOLE, // 구멍.
        NUM, // 블록이 몇 종류인지 나타낸다
    };
};

public class MapCreator : MonoBehaviour
{
    public TextAsset level_data_text = null;

    // MapCreator.cs
    public static float BLOCK_WIDTH = 1.0f; // 블록의 폭.
    public static float BLOCK_HEIGHT = 0.2f; // 블록의 높이.
    public static int BLOCK_NUM_IN_SCREEN = 30; // 화면 내에 들어가는 블록의 개수.

    private LevelControl level_control = null;

    private struct FloorBlock
    { // 블록에 관한 정보를 모아서 관리하는 구조체 (여러 개의 정보를 하나로 묶을 때 사용).
        public bool is_created; // 블록이 만들어졌는가.
        public Vector3 position; // 블록의 위치.
    };

    private FloorBlock last_block; // 마지막에 생성한 블록.
    private PlayerControl player = null; // 씬상의 Player를 보관.
    private BlockCreator block_creator; // BlockCreator를 보관.

    private GameRoot game_root = null;

    void Start()
    {
        this.player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        this.last_block.is_created = false;
        this.block_creator = this.gameObject.GetComponent<BlockCreator>();

        this.level_control = new LevelControl();
        this.level_control.initialize();

        this.level_control.loadLevelData(this.level_data_text); // 이 구문을 추가한다

        this.game_root = this.gameObject.GetComponent<GameRoot>();
        this.player.level_control = this.level_control;
    }

    void Update()
    {
        // 플레이어의 X위치를 가져온다.
        float block_generate_x = this.player.transform.position.x;
        // 그리고 대략 반 화면만큼 오른쪽으로 이동.
        // 이 위치가 블록을 생성하는 문턱 값이 된다.
        block_generate_x += BLOCK_WIDTH * ((float)BLOCK_NUM_IN_SCREEN + 1) / 2.0f;
        // 마지막에 만든 블록의 위치가 문턱 값보다 작으면.
        while (this.last_block.position.x < block_generate_x)
        {
            // 블록을 만든다.
            this.create_floor_block();
        }
    }

    private void create_floor_block()
    {
        Vector3 block_position;
        if (!this.last_block.is_created)
        {
            block_position = this.player.transform.position;
            block_position.x -= BLOCK_WIDTH * ((float)BLOCK_NUM_IN_SCREEN / 2.0f);
            block_position.y = -8.3f;
        }
        else
        {
            block_position = this.last_block.position;
        }
        block_position.x += BLOCK_WIDTH;
        // ↓ 이 행을 주석 처리(혹은 삭제).
        // this.block_creator.createBlock(block_position);
        // ↓ 볼드체 부분을 추가.
        //this.level_control.update(); // LevelControl을 갱신.
        // level_control에 저장된 current_block(지금 만들 블록 정보)의 height(높이)를 씬 상의 좌표로 변환.

        // this.level_control.update();
        this.level_control.Update(this.game_root.getPlayTime());

        block_position.y = level_control.current_block.height * BLOCK_HEIGHT - 8.3f;
        // 지금 만들 블록에 관한 정보를 변수 current에 넣는다.
        LevelControl.CreationInfo current = this.level_control.current_block;
        // 지금 만들 블록이 바닥이면 (지금 만들 블록이 구멍이라면)
        if (current.block_type == Block.TYPE.FLOOR)
        {
            // block_position의 위치에 블록을 실제로 생성.
            this.block_creator.createBlock(block_position);
        }
        this.last_block.position = block_position;
        this.last_block.is_created = true;
    }

    public bool isDelete(GameObject block_object)
    {
        bool ret = false; // 반환값.
                          // Player로부터 반 화면만큼 왼쪽에 위치, 이 위치가 사라지느냐 마느냐를 결정하는 문턱 값이 됨.
        float left_limit = this.player.transform.position.x - BLOCK_WIDTH * ((float)BLOCK_NUM_IN_SCREEN / 2.0f);
        // 블록의 위치가 문턱 값보다 작으면(왼쪽),
        if (block_object.transform.position.x < left_limit)
        {
            ret = true; // 반환값을 true(사라져도 좋다)로
        }
        return ret; // 판정 결과를 돌려줌.
    }
}
