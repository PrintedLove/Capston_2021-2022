using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;
using Random = UnityEngine.Random;

//Ÿ��
[Serializable]
public class TB
{
    public TileBase FloorTile;
}
//�÷��� ������ �����ϴ� ����
[Serializable]
public class Platform
{
    //�÷��� Y��ǥ�� �ִ�(2ĭ ������ ex : 3 �̸� 0 2 4 6 ����)
    public int Platform_Y_MAX;
    public int Platform5;
    public int Platform4;
}
public class TileMapManager : MonoBehaviour
{

    [SerializeField] private Tilemap Tilemap;
    //�ٴ� ���α��� (��� ���� �¿�, Ȧ���� ��� �����ʿ� �ϳ���)
    [SerializeField] private float FloorWidth;
    //�ٴ� ����
    [SerializeField] private int Floordepth;
    public TB tb;
    public Platform platform;

    private void Start()
    {
        Tilemap.ClearAllTiles();
        MakeFloor();
        MakePlatform();
    }
    //�ٴ��� ����� �Լ�
    private void MakeFloor()
    {
        float halfWidth = FloorWidth / 2;
        for(int e = 0 ; e < Floordepth ; e++)
        {
            for (int i = -Mathf.FloorToInt(halfWidth); i < Mathf.RoundToInt(halfWidth); i++)
            {
                Tilemap.SetTile(new Vector3Int(i, -2 - e, 0), tb.FloorTile);
            }
        }
    }  

    private void MakePlatform()
    {
        float halfWidth = FloorWidth / 2;
        //0 ~ FloorWidth-1 ���� �ִ� �迭 ����
        //-halfWidth ~ �ݿø�(halfWidth)-1 ���� ��ǥ�� ��Ÿ�� 
        //���ϴ� ��ǥ +����(halfWidth)�� �ϸ� ���ϴ� ��ǥ�� �ε����� ����
        int[] platformX = new int[(int)FloorWidth];
        //�ε��� ��ǥ�� Y��ǥ ����
        int[] platformY = new int[(int)FloorWidth];
        //�迭 �ʱ�ȭ
        //0�� �Ҵ� �ȵ� ��ǥ 1�� �Ҵ� �� ��ǥ
        for (int i = 0; i < FloorWidth; i++)
        {
            
            // -2 ~ 0 ��ǥ�� �÷��̾� ���� �����̱� ������ ����
            if(i >= -2 + halfWidth && i <= 1 + halfWidth)
            {
                platformX[i] = 1;
            }
            else
            {
                platformX[i] = 0;
            }
            //�⺻ �ٴ� ��ǥ�� �ʱ�ȭ
            platformY[i] = -2;
        }
        //����ȸ���� �ʹ� ������ Ż���ϱ� ���� ����
        int escape = 0;
        //ũ�Ⱑ 5�� �÷��� ����
        for (int i = 0 ; i < platform.Platform5 ; i++)
        {
            //�������� X,Y ��ǥ�� �� Y��ǥ�� 2ĭ ��������
            int R_X = Random.Range(8, (int)FloorWidth -8);
            int R_Y = Random.Range(0, platform.Platform_Y_MAX) * 2;
            //üũ�� �� ������ �Ҵ�� ��ǥ�� �ִ��� ��Ÿ���� ����
            bool X_Assignment = false;
            //���� ��ġ�� �������� üũ�ϴ� ����
            bool Y_able = true;

            //5ĭ �̳��� �Ҵ�� ��ǥ�� �ִ��� üũ
            for (int e = 0; e < 5; e++)
            {
                if (platformX[R_X + e - 2] == 1)
                {
                    //üũ for�� Ż�� 
                    e = 5;
                    //���� ��ġ�� �������� i�� ������ ���� -1
                    i--;
                    escape++;

                    X_Assignment = true;
                }
            }
            //���� ��ġ�� �������� üũ
            if (!X_Assignment)
            {
                for(int e = 0 ; e < R_Y / 2 ; e++)
                {
                    //���ʿ� �Ҵ�� ���� üũ
                    if (platformX[R_X - e - 3] == 1)
                    {
                        //���ʿ��� e + 1 ��° ������ Y ��ǥ ���̰� 2 + e*2 �ʰ� �̸� ��ġ �Ұ���
                        if(Mathf.Abs(platformY[R_X - e - 3] - R_Y) > 2 + e*2 )
                        {
                            Y_able = false;
                            //���� ��ġ�� �Ұ����� �÷��� ��ġ�� ���ؼ� i�� ������ ���� -1
                            i--;
                            escape++;
                        }
                        
                    }
                    //�����ʿ� �Ҵ�� ���� üũ
                    if (platformX[R_X + e + 3] == 1)
                    {
                        //�����ʿ��� e + 1 ��° ������ Y ��ǥ ���̰� 2 + e*2 �ʰ� �̸� ��ġ �Ұ���
                        if (Mathf.Abs(platformY[R_X + e + 3] - R_Y) > 2 + e*2)
                        {
                            Y_able = false;
                            //���� ��ġ�� �Ұ����� �÷��� ��ġ�� ���ؼ� i�� ������ ���� -1
                            i--;
                            escape++;
                        }

                    }
                }    
            }
            
            //5ĭ �ȿ� �Ҵ�� ��ǥ�� ���� ������ ��ġ �ؾ� �� ��� ��ġ �����Ҷ� �÷��� ��ġ �� �Ҵ�
            if (!X_Assignment && Y_able)
            {
                //���ʿ� ������ �ִ��� üũ �ϴ� ����
                bool RplatformExist = false;
                bool LplatformExist = false;
                int RplatformX = 0;
                int RplatformY = 0;
                int LplatformX = 0;
                int LplatformY = 0;
                for (int e = 0; e < R_Y / 2; e++)
                {
                    //�÷��� ���ʺ��� ���ʿ� �Ҵ�� ���� üũ
                    if (platformX[R_X - e - 3] == 1 && LplatformExist == false)
                    {
                        LplatformExist = true;
                        LplatformX = e;
                        LplatformY = platformY[R_X - e - 3];
                    }
                    //�÷��� �����ʺ��� �����ʿ� �Ҵ�� ���� üũ
                    if (platformX[R_X + e + 3] == 1 && RplatformExist == false)
                    {
                        RplatformExist = true;
                        RplatformX = e;
                        RplatformY = platformY[R_X + e + 3];
                    }
                }
                for (int e = 0; e < R_Y / 2; e++)
                {
                    if (!LplatformExist)
                    {
                        //���� ���ʺ��� ������ ���� ������ �ʿ��ҽ� ��ġ
                        Tilemap.SetTile(new Vector3Int(R_X - Mathf.FloorToInt(halfWidth) - 2 - (R_Y / 2 - e), 2 * e, 0), tb.FloorTile);
                        platformX[R_X - 2 - (R_Y / 2 - e)] = 1;
                        platformY[R_X - 2 - (R_Y / 2 - e)] = 2 * e;
                    }
                    else
                    {
                        //���ʿ� �Ÿ��� 1ĭ �̻� ������ ������ ������ ���Ǽ�ġ  
                        if(LplatformX >= 1)
                        {
                            for (int o = 0 ; o < LplatformX ; o++)
                            {
                                //2ĭ ���� ����
                                if ((LplatformY - R_Y) / (LplatformX + 1) < 2)
                                {
                                    Tilemap.SetTile(new Vector3Int(R_X - Mathf.FloorToInt(halfWidth) - 3 - e, R_Y + (LplatformY - R_Y) / Mathf.Abs((LplatformY - R_Y)) * 2, 0), tb.FloorTile);
                                    platformX[R_X - 3 - e] = 1;
                                    platformY[R_X - 3 - e] = R_Y + (LplatformY - R_Y) / Mathf.Abs((LplatformY - R_Y)) * 2;
                                }
                                //2ĭ �ʰ� ���� ����
                                else
                                {
                                    Tilemap.SetTile(new Vector3Int(R_X - Mathf.FloorToInt(halfWidth) - 3 - e, R_Y + (LplatformY - R_Y) / (LplatformX + 1), 0), tb.FloorTile);
                                    platformX[R_X - 3 - e] = 1;
                                    platformY[R_X - 3 - e] = R_Y + (LplatformY - R_Y) / (LplatformX + 1);
                                }
                                
                            }
                        }  
                    }
                    if (!RplatformExist)
                    {
                        //���� �����ʺ��� ������ ���� ������ �ʿ��ҽ� ��ġ
                        Tilemap.SetTile(new Vector3Int(R_X - Mathf.FloorToInt(halfWidth) + 2 + (R_Y / 2 - e), 2 * e, 0), tb.FloorTile);
                        platformX[R_X + 2 + (R_Y / 2 - e)] = 1;
                        platformY[R_X + 2 + (R_Y / 2 - e)] = 2 * e;
                    }
                    else
                    {
                        //�����ʿ� �Ÿ��� 1ĭ �̻� ������ ������ ������ ���Ǽ�ġ  
                        if (RplatformX >= 1)
                        {
                            for (int o = 0; o < RplatformX; o++)
                            {
                                //2ĭ ���� ����
                                if ((LplatformY - R_Y) / (LplatformX + 1) < 2)
                                {
                                    Tilemap.SetTile(new Vector3Int(R_X - Mathf.FloorToInt(halfWidth) - 3 - e, R_Y + (RplatformY - R_Y) / Mathf.Abs((RplatformY - R_Y)) * 2, 0), tb.FloorTile);
                                    platformX[R_X - 3 - e] = 1;
                                    platformY[R_X - 3 - e] = R_Y + (RplatformY - R_Y) / Mathf.Abs((RplatformY - R_Y)) * 2;
                                }
                                //2ĭ �ʰ� ���� ����
                                else
                                {
                                    Tilemap.SetTile(new Vector3Int(R_X - Mathf.FloorToInt(halfWidth) - 3 - e, R_Y + (RplatformY - R_Y) / (RplatformX + 1), 0), tb.FloorTile);
                                    platformX[R_X - 3 - e] = 1;
                                    platformY[R_X - 3 - e] = R_Y + (RplatformY - R_Y) / (RplatformX + 1);
                                }
                            }
                        }
                    }
                }

                Tilemap.SetTile(new Vector3Int(R_X - Mathf.FloorToInt(halfWidth), R_Y, 0), tb.FloorTile);
                Tilemap.SetTile(new Vector3Int(R_X - Mathf.FloorToInt(halfWidth) - 1, R_Y, 0), tb.FloorTile);
                Tilemap.SetTile(new Vector3Int(R_X - Mathf.FloorToInt(halfWidth) - 2, R_Y, 0), tb.FloorTile);
                Tilemap.SetTile(new Vector3Int(R_X - Mathf.FloorToInt(halfWidth) + 1, R_Y, 0), tb.FloorTile);
                Tilemap.SetTile(new Vector3Int(R_X - Mathf.FloorToInt(halfWidth) + 2, R_Y, 0), tb.FloorTile);
                for(int e = 0 ; e < 5 ; e++)
                {
                    platformX[R_X + e - 2] = 1;
                    platformY[R_X + e - 2] = R_Y;
                }
            }  
            //����ȸ���� �ʹ� ������� Ż��
            if(escape > 100)
            {
                break;
            }
        }
    }
}
