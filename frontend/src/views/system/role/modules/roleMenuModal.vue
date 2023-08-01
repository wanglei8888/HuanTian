<template>
  <a-modal
    :title="title"
    :width="600"
    :visible="visible"
    :confirmLoading="confirmLoading"
    @ok="handleOk"
    @cancel="handleCancel">
    <a-row>
      <a-col>
        <a-button type="primary" @click="closeAll">收起全部</a-button>
      </a-col>
    </a-row>
    <a-spin :spinning="loading">
      <a-tree
        style="margin-left: 60px;margin-top: 16px;"
        v-model="checkedKeys"
        checkable
        ref="tree"
        :expanded-keys="expandedKeys"
        :tree-data="treeData"
        :replaceFields="{
          key: 'id',
          title: 'name',
          value: 'id'
        }"
        @expand="onExpand"
      />
    </a-spin>
  </a-modal>
</template>
<script>
export default {
  data () {
    return {
      labelCol: {
        md: { span: 6 },
        sm: { span: 4 }
      },
      wrapperCol: {
        md: { span: 14 },
        sm: { span: 20 }
      },
      gutter: 24,
      mdSize: 24,
      smSize: 24,
      checkedKeys: [],
      expandedKeys: [],
      title: '角色菜单',
      visible: false,
      confirmLoading: false,
      loading: false,
      id: 0,
      treeData: []
    }
  },
  methods: {
    // 打开页面初始化
    create (value) {
      this.id = value
      this.visible = true
      this.confirmLoading = false
      this.treeData = []
      this.handSerach()
      this.getOwnMenu()
    },
    // 获取已有菜单
    async getOwnMenu () {
      const res = await this.$http.get('/sysMenu/roleMenu', { params: { roleId: this.id } })
      this.checkedKeys = res.result.map(t => t.id)
    },
    // 查询数据
    async handSerach () {
      this.loading = true
      const res = await this.$http.get('/sysMenu/tree').finally(() => {
        this.loading = false
      })
      this.treeData = res.result
    },
    close () {
      this.$emit('close')
      this.visible = false
    },
    onExpand (expandedKeys) {
      this.expandedKeys = expandedKeys
    },
    async handleOk () {
      if (this.checkedKeys.length === 0) {
        this.$message.warning('请选择菜单')
        return
      }
      this.confirmLoading = true
      const res = await this.$http.post('/sysRole/addRoleMenu', { roleId: this.id, menuId: this.checkedKeys }).finally(() => { this.confirmLoading = false })
      if (res.code === 200) {
        this.$emit('ok')
        this.$message.success('授权成功')
        this.visible = false
      }
    },
    handleCancel () {
      this.close()
    },
    closeAll () {
      this.expandedKeys = [] // 清空展开的节点
    }
  }
}
</script>

<style scoped lang="less">
</style>
