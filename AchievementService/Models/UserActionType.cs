namespace AchievementService.Models;

public enum UserActionType
{
    NewLoginItemSave = 1000,
    NewCardItemSave = 1001,
    NewIdentityItemSave = 1002,
    NewNoteItemSave = 1003,
    VaultItemDelete = 1004,
    ItemSend = 1005,
    VaultItemFavorite = 1006,
    AddItemToFolder = 1007,
    AutofillUsed = 1008,
    GeneratorUsed = 1009,
    AddUriToItem = 10010,
    AddAttachmentToItem = 10011
}