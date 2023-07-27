
export function removeEmptyChildren (data) {
  if (data == null || data.length === 0) return
  for (let i = 0; i < data.length; i++) {
    const item = data[i]
    if (item.children != null && item.children.length === 0) {
      item.children = null
    } else {
      removeEmptyChildren(item.children)
    }
  }
}
